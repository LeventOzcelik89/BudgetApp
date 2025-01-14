namespace BudgetApp.API.Services;

using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Configuration;

public class FirebasePushNotificationService : IPushNotificationService
{
    private readonly FirebaseMessaging _messaging;
    private readonly IDeviceService _deviceService;
    private readonly IConfiguration _configuration;

    public FirebasePushNotificationService(
        IDeviceService deviceService,
        IConfiguration configuration)
    {
        _deviceService = deviceService;
        _configuration = configuration;

        if (FirebaseApp.DefaultInstance == null)
        {
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(_configuration["Firebase:CredentialsPath"])
            });
        }
        _messaging = FirebaseMessaging.DefaultInstance;
    }

    public async Task SendAsync(IEnumerable<string> deviceTokens, string title, string message, object data = null)
    {
        if (!deviceTokens.Any()) return;

        var notification = new MulticastMessage()
        {
            Tokens = deviceTokens.ToList(),
            Notification = new Notification
            {
                Title = title,
                Body = message
            },
            Data = data != null ? ToDictionary(data) : null
        };

        try
        {
            var response = await _messaging.SendMulticastAsync(notification);
            
            // Başarısız olan token'ları devre dışı bırak
            if (response.FailureCount > 0)
            {
                for (var i = 0; i < response.Responses.Count; i++)
                {
                    if (!response.Responses[i].IsSuccess)
                    {
                        var failedToken = deviceTokens.ElementAt(i);
                        // Token geçersiz ise cihazı devre dışı bırak
                        if (response.Responses[i].Exception?.MessagingErrorCode == MessagingErrorCode.Unregistered)
                        {
                            // TODO: Cihazı devre dışı bırak
                            // await _deviceService.DeactivateDeviceAsync(userId, failedToken);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log the error
            throw new Exception("Failed to send push notification", ex);
        }
    }

    public async Task SendToUserAsync(int userId, string title, string message, object data = null)
    {
        var deviceTokens = await _deviceService.GetActiveDeviceTokensAsync(userId);
        await SendAsync(deviceTokens, title, message, data);
    }

    public async Task SendToAllAsync(string title, string messageText, object data = null)
    {
        var fcmMessage = new Message()
        {
            Topic = "all",
            Notification = new Notification
            {
                Title = title,
                Body = messageText
            },
            Data = data != null ? ToDictionary(data) : null
        };

        try
        {
            await _messaging.SendAsync(fcmMessage);
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to send broadcast notification", ex);
        }
    }

    private Dictionary<string, string> ToDictionary(object obj)
    {
        return obj.GetType()
            .GetProperties()
            .ToDictionary(
                prop => prop.Name,
                prop => prop.GetValue(obj)?.ToString()
            );
    }
} 
namespace BudgetApp.API.Services;

public interface IPushNotificationService
{
    Task SendAsync(IEnumerable<string> deviceTokens, string title, string message, object data = null);
    Task SendToUserAsync(int userId, string title, string message, object data = null);
    Task SendToAllAsync(string title, string message, object data = null);
} 
namespace BudgetApp.API.Models;

using BudgetApp.API.Models.Common;
using BudgetApp.API.DTOs.Settings;
using System.Text.Json;

public class UserSettings : BaseEntity
{
    public int UserId { get; set; }
    public string CurrencyCode { get; set; } = "TRY";
    public string Language { get; set; } = "tr";
    public string TimeZone { get; set; } = "Europe/Istanbul";
    public string NotificationPreferencesJson { get; set; }
    public string BudgetPreferencesJson { get; set; }

    public User User { get; set; }
    public Currency CurrencyInfo { get; set; }

    public NotificationPreferences GetNotificationPreferences()
    {
        return string.IsNullOrEmpty(NotificationPreferencesJson)
            ? new NotificationPreferences()
            : JsonSerializer.Deserialize<NotificationPreferences>(NotificationPreferencesJson);
    }

    public void SetNotificationPreferences(NotificationPreferences preferences)
    {
        NotificationPreferencesJson = JsonSerializer.Serialize(preferences);
    }

    public BudgetPreferences GetBudgetPreferences()
    {
        return string.IsNullOrEmpty(BudgetPreferencesJson)
            ? new BudgetPreferences()
            : JsonSerializer.Deserialize<BudgetPreferences>(BudgetPreferencesJson);
    }

    public void SetBudgetPreferences(BudgetPreferences preferences)
    {
        BudgetPreferencesJson = JsonSerializer.Serialize(preferences);
    }
} 
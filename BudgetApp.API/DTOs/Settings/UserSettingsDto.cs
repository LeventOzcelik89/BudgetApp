namespace BudgetApp.API.DTOs.Settings;

public class UserSettingsDto
{
    public int Id { get; set; }
    public string CurrencyCode { get; set; }
    public string Language { get; set; }
    public string TimeZone { get; set; }
    public NotificationPreferences NotificationPreferences { get; set; }
    public BudgetPreferences BudgetPreferences { get; set; }
    public CurrencyInfo Currency { get; set; }
}

public class CurrencyInfo
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
    public string Flag { get; set; }
}

public class NotificationPreferences
{
    public bool EnablePushNotifications { get; set; } = true;
    public bool EnableEmailNotifications { get; set; } = true;
    public bool NotifyOnBudgetExceeded { get; set; } = true;
    public bool NotifyOnGoalProgress { get; set; } = true;
    public bool NotifyOnLargeTransactions { get; set; } = true;
    public decimal LargeTransactionThreshold { get; set; } = 1000;
}

public class BudgetPreferences
{
    public bool AutomaticallyCategorizeTransactions { get; set; } = true;
    public bool WarnWhenBudgetExceeds { get; set; } = true;
    public int WarnAtPercentage { get; set; } = 80;
    public bool RolloverUnusedBudget { get; set; } = false;
}

public class UpdateUserSettingsDto
{
    public string CurrencyCode { get; set; }
    public string Language { get; set; }
    public string TimeZone { get; set; }
    public NotificationPreferences NotificationPreferences { get; set; }
    public BudgetPreferences BudgetPreferences { get; set; }
} 
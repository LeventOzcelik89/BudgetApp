namespace BudgetApp.API.DTOs.Notifications;

public class NotificationDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public NotificationType Type { get; set; }
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateNotificationDto
{
    public string Title { get; set; }
    public string Message { get; set; }
    public NotificationType Type { get; set; }
}

public enum NotificationType
{
    BudgetAlert = 1,
    GoalProgress = 2,
    GoalAchieved = 3,
    TransactionAlert = 4,
    SystemNotification = 5
} 
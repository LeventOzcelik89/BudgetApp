namespace BudgetApp.API.Services;

using BudgetApp.API.DTOs.Notifications;

public interface INotificationService
{
    Task<IEnumerable<NotificationDto>> GetAllAsync(int userId);
    Task<IEnumerable<NotificationDto>> GetUnreadAsync(int userId);
    Task<NotificationDto> CreateAsync(int userId, CreateNotificationDto dto);
    Task MarkAsReadAsync(int userId, int notificationId);
    Task MarkAllAsReadAsync(int userId);
    Task DeleteAsync(int userId, int notificationId);

    // Özel bildirim metodları
    Task NotifyBudgetExceededAsync(int userId, int categoryId, decimal amount, decimal limit);
    Task NotifyGoalProgressAsync(int userId, int goalId, decimal progress);
    Task NotifyGoalAchievedAsync(int userId, int goalId);
    Task NotifyLargeTransactionAsync(int userId, decimal amount, string categoryName);
}
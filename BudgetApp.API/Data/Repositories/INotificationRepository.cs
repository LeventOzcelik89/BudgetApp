namespace BudgetApp.API.Data.Repositories;

using BudgetApp.API.Models;

public interface INotificationRepository : IRepository<Notification>
{
    Task<IEnumerable<Notification>> GetByUserIdAsync(int userId);
    Task<IEnumerable<Notification>> GetUnreadByUserIdAsync(int userId);
    Task MarkAsReadAsync(int notificationId);
    Task MarkAllAsReadAsync(int userId);
} 
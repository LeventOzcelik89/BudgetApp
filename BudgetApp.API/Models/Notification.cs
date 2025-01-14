namespace BudgetApp.API.Models;

using BudgetApp.API.Models.Common;
using BudgetApp.API.DTOs.Notifications;

public class Notification : BaseEntity
{
    public int UserId { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public NotificationType Type { get; set; }
    public bool IsRead { get; set; }

    public User User { get; set; }
} 
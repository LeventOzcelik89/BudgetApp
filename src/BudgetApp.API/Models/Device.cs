namespace BudgetApp.API.Models;

using BudgetApp.API.Models.Common;

public class Device : BaseEntity
{
    public int UserId { get; set; }
    public string DeviceToken { get; set; }
    public string DeviceType { get; set; }
    public bool IsActive { get; set; }
    public DateTime LastUsedAt { get; set; }

    public User User { get; set; }
} 
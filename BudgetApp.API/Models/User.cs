namespace BudgetApp.API.Models;

using BudgetApp.API.Models.Common;

public class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }
    public bool EmailConfirmed { get; set; }
    public DateTime? LastLoginDate { get; set; }
} 
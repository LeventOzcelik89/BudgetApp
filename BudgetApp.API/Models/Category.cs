namespace BudgetApp.API.Models;

using BudgetApp.API.Models.Common;
using BudgetApp.API.Models.Enums;

public class Category : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int UserId { get; set; }
    public CategoryType Type { get; set; }

    public User User { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
} 
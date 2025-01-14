namespace BudgetApp.API.Models;

using BudgetApp.API.Models.Common;

public class Budget : BaseEntity
{
    public int UserId { get; set; }
    public int CategoryId { get; set; }
    public decimal PlannedAmount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public User User { get; set; }
    public Category Category { get; set; }
} 
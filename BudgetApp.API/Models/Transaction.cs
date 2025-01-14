namespace BudgetApp.API.Models;

using BudgetApp.API.Models.Common;
using BudgetApp.API.Models.Enums;

public class Transaction : BaseEntity
{
    public int UserId { get; set; }
    public decimal OriginalAmount { get; set; }
    public decimal ConvertedAmount { get; set; }
    public string Description { get; set; }
    public DateTime TransactionDate { get; set; }
    public TransactionType Type { get; set; }
    public int? CategoryId { get; set; }
    public string CurrencyCode { get; set; }
    public decimal ExchangeRate { get; set; } = 1;

    public User User { get; set; }
    public Category Category { get; set; }
    public Currency Currency { get; set; }
} 
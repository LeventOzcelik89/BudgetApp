namespace BudgetApp.API.DTOs.Transaction;
using BudgetApp.API.Models.Enums;
using BudgetApp.API.DTOs.Category;
using BudgetApp.API.DTOs.Settings;

public class TransactionDto
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public DateTime TransactionDate { get; set; }
    public TransactionType Type { get; set; }
    public int? CategoryId { get; set; }
    public string CategoryName { get; set; }
    public string CurrencyCode { get; set; }
    public decimal OriginalAmount { get; set; }
    public decimal ConvertedAmount { get; set; }
    public decimal ExchangeRate { get; set; }
    public CurrencyInfo Currency { get; set; }
}

public class CreateTransactionDto
{
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public DateTime TransactionDate { get; set; }
    public TransactionType Type { get; set; }
    public int? CategoryId { get; set; }
    public string CurrencyCode { get; set; }
}

public class UpdateTransactionDto
{
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public DateTime TransactionDate { get; set; }
    public TransactionType Type { get; set; }
    public int? CategoryId { get; set; }
}

public class TransactionSummaryDto
{
    public decimal TotalIncome { get; set; }
    public decimal TotalExpense { get; set; }
    public decimal Balance { get; set; }
    public CurrencyInfo Currency { get; set; }
    public List<CategorySummaryDto> CategorySummaries { get; set; }
}

public class CategorySummaryDto
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public decimal Amount { get; set; }
    public decimal Percentage { get; set; }
    public CurrencyInfo Currency { get; set; }
} 
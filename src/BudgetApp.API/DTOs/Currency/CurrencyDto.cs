namespace BudgetApp.API.DTOs.Currency;

public class CurrencyDto
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
    public string Flag { get; set; }
    public bool IsActive { get; set; }
    public int DisplayOrder { get; set; }
}

public class UpdateCurrencyDto
{
    public bool IsActive { get; set; }
    public int DisplayOrder { get; set; }
} 
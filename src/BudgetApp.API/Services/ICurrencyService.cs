namespace BudgetApp.API.Services;

using BudgetApp.API.DTOs.Currency;

public interface ICurrencyService
{
    Task<IEnumerable<CurrencyDto>> GetAllAsync();
    Task<IEnumerable<CurrencyDto>> GetActiveCurrenciesAsync();
    Task<CurrencyDto> GetByCodeAsync(string code);
    Task<CurrencyDto> UpdateAsync(string code, UpdateCurrencyDto dto);
} 
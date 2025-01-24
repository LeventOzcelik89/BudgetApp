namespace BudgetApp.API.Data.Repositories;

using BudgetApp.API.Models;

public interface ICurrencyRepository : IRepository<Currency>
{
    Task<IEnumerable<Currency>> GetActiveCurrenciesAsync();
    Task<Currency> GetByCodeAsync(string code);
} 
namespace BudgetApp.API.Data.Repositories;

using Microsoft.EntityFrameworkCore;
using BudgetApp.API.Models;

public class CurrencyRepository : Repository<Currency>, ICurrencyRepository
{
    public CurrencyRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Currency>> GetActiveCurrenciesAsync()
    {
        return await _dbSet
            .Where(c => c.IsActive && !c.IsDeleted)
            .OrderBy(c => c.DisplayOrder)
            .ToListAsync();
    }

    public async Task<Currency> GetByCodeAsync(string code)
    {
        return await _dbSet
            .FirstOrDefaultAsync(c => c.Code == code && !c.IsDeleted);
    }
} 
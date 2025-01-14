namespace BudgetApp.API.Data.Repositories;

using Microsoft.EntityFrameworkCore;
using BudgetApp.API.Models;
using BudgetApp.API.DTOs.Settings;

public class UserSettingsRepository : Repository<UserSettings>, IUserSettingsRepository
{
    public UserSettingsRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<UserSettings> GetByUserIdAsync(int userId)
    {
        return await _dbSet
            .Include(s => s.CurrencyInfo)
            .FirstOrDefaultAsync(s => s.UserId == userId && !s.IsDeleted);
    }

    public async Task<UserSettings> CreateDefaultSettingsAsync(int userId)
    {
        var settings = new UserSettings
        {
            UserId = userId,
            CurrencyCode = "TRY",
            Language = "tr",
            TimeZone = "Europe/Istanbul"
        };

        settings.SetNotificationPreferences(new NotificationPreferences());
        settings.SetBudgetPreferences(new BudgetPreferences());

        await AddAsync(settings);
        return settings;
    }
} 
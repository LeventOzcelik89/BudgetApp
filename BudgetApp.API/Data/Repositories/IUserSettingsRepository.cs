namespace BudgetApp.API.Data.Repositories;

using BudgetApp.API.Models;

public interface IUserSettingsRepository : IRepository<UserSettings>
{
    Task<UserSettings> GetByUserIdAsync(int userId);
    Task<UserSettings> CreateDefaultSettingsAsync(int userId);
} 
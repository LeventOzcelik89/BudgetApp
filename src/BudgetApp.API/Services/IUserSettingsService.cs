namespace BudgetApp.API.Services;

using BudgetApp.API.DTOs.Settings;

public interface IUserSettingsService
{
    Task<UserSettingsDto> GetByUserIdAsync(int userId);
    Task<UserSettingsDto> UpdateAsync(int userId, UpdateUserSettingsDto dto);
    Task<UserSettingsDto> CreateDefaultSettingsAsync(int userId);
} 
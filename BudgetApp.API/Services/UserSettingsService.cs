namespace BudgetApp.API.Services;

using AutoMapper;
using BudgetApp.API.Data.Repositories;
using BudgetApp.API.DTOs.Settings;
using BudgetApp.API.Models;

public class UserSettingsService : IUserSettingsService
{
    private readonly IUserSettingsRepository _userSettingsRepository;
    private readonly ICurrencyRepository _currencyRepository;
    private readonly IMapper _mapper;

    public UserSettingsService(
        IUserSettingsRepository userSettingsRepository,
        ICurrencyRepository currencyRepository,
        IMapper mapper)
    {
        _userSettingsRepository = userSettingsRepository;
        _currencyRepository = currencyRepository;
        _mapper = mapper;
    }

    public async Task<UserSettingsDto> GetByUserIdAsync(int userId)
    {
        var settings = await _userSettingsRepository.GetByUserIdAsync(userId);
        if (settings == null)
            return null;

        var currency = await _currencyRepository.GetByCodeAsync(settings.CurrencyCode);
        var dto = _mapper.Map<UserSettingsDto>(settings);
        dto.Currency = _mapper.Map<CurrencyInfo>(currency ?? settings.CurrencyInfo);

        return dto;
    }

    public async Task<UserSettingsDto> UpdateAsync(int userId, UpdateUserSettingsDto dto)
    {
        var settings = await _userSettingsRepository.GetByUserIdAsync(userId);
        if (settings == null)
            throw new Exception("Settings not found");

        // Para birimi değiştirilmek isteniyorsa, geçerli bir para birimi olduğunu kontrol et
        if (!string.IsNullOrEmpty(dto.CurrencyCode) && dto.CurrencyCode != settings.CurrencyCode)
        {
            var currency = await _currencyRepository.GetByCodeAsync(dto.CurrencyCode);
            if (currency == null || !currency.IsActive)
                throw new Exception("Invalid or inactive currency code");

            settings.CurrencyCode = dto.CurrencyCode;
        }

        settings.Language = dto.Language ?? settings.Language;
        settings.TimeZone = dto.TimeZone ?? settings.TimeZone;

        if (dto.NotificationPreferences != null)
            settings.SetNotificationPreferences(dto.NotificationPreferences);

        if (dto.BudgetPreferences != null)
            settings.SetBudgetPreferences(dto.BudgetPreferences);

        await _userSettingsRepository.UpdateAsync(settings);

        return await GetByUserIdAsync(userId);
    }

    public async Task<UserSettingsDto> CreateDefaultSettingsAsync(int userId)
    {
        var settings = await _userSettingsRepository.CreateDefaultSettingsAsync(userId);
        return await GetByUserIdAsync(userId);
    }
} 
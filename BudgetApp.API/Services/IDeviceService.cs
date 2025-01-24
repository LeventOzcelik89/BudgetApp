namespace BudgetApp.API.Services;

using BudgetApp.API.DTOs.Devices;

public interface IDeviceService
{
    Task<IEnumerable<DeviceDto>> GetAllAsync(int userId);
    Task<DeviceDto> RegisterDeviceAsync(int userId, RegisterDeviceDto dto);
    Task<DeviceDto> UpdateDeviceAsync(int userId, string token, UpdateDeviceDto dto);
    Task DeactivateDeviceAsync(int userId, string token);
    Task UpdateLastUsedAsync(string token);
    Task<IEnumerable<string>> GetActiveDeviceTokensAsync(int userId);
} 
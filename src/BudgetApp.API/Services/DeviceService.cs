namespace BudgetApp.API.Services;

using AutoMapper;
using BudgetApp.API.Data.Repositories;
using BudgetApp.API.DTOs.Devices;
using BudgetApp.API.Models;

public class DeviceService : IDeviceService
{
    private readonly IDeviceRepository _deviceRepository;
    private readonly IMapper _mapper;

    public DeviceService(IDeviceRepository deviceRepository, IMapper mapper)
    {
        _deviceRepository = deviceRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DeviceDto>> GetAllAsync(int userId)
    {
        var devices = await _deviceRepository.GetByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<DeviceDto>>(devices);
    }

    public async Task<DeviceDto> RegisterDeviceAsync(int userId, RegisterDeviceDto dto)
    {
        var existingDevice = await _deviceRepository.GetByTokenAsync(dto.DeviceToken);
        if (existingDevice != null)
        {
            existingDevice.IsActive = true;
            existingDevice.LastUsedAt = DateTime.UtcNow;
            await _deviceRepository.UpdateAsync(existingDevice);
            return _mapper.Map<DeviceDto>(existingDevice);
        }

        var device = new Device
        {
            UserId = userId,
            DeviceToken = dto.DeviceToken,
            DeviceType = dto.DeviceType,
            IsActive = true,
            LastUsedAt = DateTime.UtcNow
        };

        await _deviceRepository.AddAsync(device);
        return _mapper.Map<DeviceDto>(device);
    }

    public async Task<DeviceDto> UpdateDeviceAsync(int userId, string token, UpdateDeviceDto dto)
    {
        var device = await _deviceRepository.GetByTokenAsync(token);
        if (device == null || device.UserId != userId)
            throw new Exception("Device not found");

        device.DeviceToken = dto.DeviceToken;
        device.IsActive = dto.IsActive;
        device.LastUsedAt = DateTime.UtcNow;

        await _deviceRepository.UpdateAsync(device);
        return _mapper.Map<DeviceDto>(device);
    }

    public async Task DeactivateDeviceAsync(int userId, string token)
    {
        var device = await _deviceRepository.GetByTokenAsync(token);
        if (device == null || device.UserId != userId)
            throw new Exception("Device not found");

        await _deviceRepository.DeactivateDeviceAsync(token);
    }

    public async Task UpdateLastUsedAsync(string token)
    {
        var device = await _deviceRepository.GetByTokenAsync(token);
        if (device != null)
        {
            await _deviceRepository.UpdateLastUsedAsync(device.Id);
        }
    }

    public async Task<IEnumerable<string>> GetActiveDeviceTokensAsync(int userId)
    {
        var devices = await _deviceRepository.GetActiveDevicesByUserIdAsync(userId);
        return devices.Select(d => d.DeviceToken);
    }
} 
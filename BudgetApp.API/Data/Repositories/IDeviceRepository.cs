namespace BudgetApp.API.Data.Repositories;

using BudgetApp.API.Models;

public interface IDeviceRepository : IRepository<Device>
{
    Task<IEnumerable<Device>> GetByUserIdAsync(int userId);
    Task<Device> GetByTokenAsync(string token);
    Task<IEnumerable<Device>> GetActiveDevicesByUserIdAsync(int userId);
    Task UpdateLastUsedAsync(int deviceId);
    Task DeactivateDeviceAsync(string token);
} 
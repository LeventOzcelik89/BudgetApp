namespace BudgetApp.API.Data.Repositories;

using Microsoft.EntityFrameworkCore;
using BudgetApp.API.Models;

public class DeviceRepository : Repository<Device>, IDeviceRepository
{
    public DeviceRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Device>> GetByUserIdAsync(int userId)
    {
        return await _dbSet
            .Where(d => d.UserId == userId && !d.IsDeleted)
            .OrderByDescending(d => d.LastUsedAt)
            .ToListAsync();
    }

    public async Task<Device> GetByTokenAsync(string token)
    {
        return await _dbSet
            .FirstOrDefaultAsync(d => d.DeviceToken == token && !d.IsDeleted);
    }

    public async Task<IEnumerable<Device>> GetActiveDevicesByUserIdAsync(int userId)
    {
        return await _dbSet
            .Where(d => d.UserId == userId && !d.IsDeleted && d.IsActive)
            .ToListAsync();
    }

    public async Task UpdateLastUsedAsync(int deviceId)
    {
        var device = await _dbSet.FindAsync(deviceId);
        if (device != null)
        {
            device.LastUsedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeactivateDeviceAsync(string token)
    {
        var device = await GetByTokenAsync(token);
        if (device != null)
        {
            device.IsActive = false;
            await _context.SaveChangesAsync();
        }
    }
} 
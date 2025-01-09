namespace BudgetApp.API.Data.Repositories;

using BudgetApp.API.Models;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetByEmailAsync(string email);
    Task<bool> EmailExistsAsync(string email);
} 
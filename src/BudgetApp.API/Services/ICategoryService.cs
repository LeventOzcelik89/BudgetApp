namespace BudgetApp.API.Services;

using BudgetApp.API.DTOs.Category;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllAsync(int userId);
    Task<CategoryDto> GetByIdAsync(int userId, int categoryId);
    Task<CategoryDto> CreateAsync(int userId, CreateCategoryDto dto);
    Task<CategoryDto> UpdateAsync(int userId, int categoryId, UpdateCategoryDto dto);
    Task DeleteAsync(int userId, int categoryId);
} 
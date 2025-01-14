namespace BudgetApp.API.Services;

using AutoMapper;
using BudgetApp.API.Data.Repositories;
using BudgetApp.API.DTOs.Category;
using BudgetApp.API.Models;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync(int userId)
    {
        var categories = await _categoryRepository.GetByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<CategoryDto>>(categories);
    }

    public async Task<CategoryDto> GetByIdAsync(int userId, int categoryId)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);
        if (category == null || category.UserId != userId)
            throw new Exception("Category not found");

        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto> CreateAsync(int userId, CreateCategoryDto dto)
    {
        var category = _mapper.Map<Category>(dto);
        category.UserId = userId;

        await _categoryRepository.AddAsync(category);
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto> UpdateAsync(int userId, int categoryId, UpdateCategoryDto dto)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);
        if (category == null || category.UserId != userId)
            throw new Exception("Category not found");

        _mapper.Map(dto, category);
        await _categoryRepository.UpdateAsync(category);

        return _mapper.Map<CategoryDto>(category);
    }

    public async Task DeleteAsync(int userId, int categoryId)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);
        if (category == null || category.UserId != userId)
            throw new Exception("Category not found");

        await _categoryRepository.DeleteAsync(categoryId);
    }
} 
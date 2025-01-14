namespace BudgetApp.API.Services;

using AutoMapper;
using BudgetApp.API.Data.Repositories;
using BudgetApp.API.DTOs.Goals;
using BudgetApp.API.Models;

public class GoalService : IGoalService
{
    private readonly IGoalRepository _goalRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GoalService(
        IGoalRepository goalRepository,
        ICategoryRepository categoryRepository,
        IMapper mapper)
    {
        _goalRepository = goalRepository;
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GoalDto>> GetAllAsync(int userId)
    {
        var goals = await _goalRepository.GetByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<GoalDto>>(goals);
    }

    public async Task<GoalDto> GetByIdAsync(int userId, int goalId)
    {
        var goal = await _goalRepository.GetByIdAsync(goalId);
        if (goal == null || goal.UserId != userId)
            throw new Exception("Goal not found");

        return _mapper.Map<GoalDto>(goal);
    }

    public async Task<IEnumerable<GoalDto>> GetActiveGoalsAsync(int userId)
    {
        var goals = await _goalRepository.GetActiveGoalsAsync(userId);
        return _mapper.Map<IEnumerable<GoalDto>>(goals);
    }

    public async Task<GoalDto> CreateAsync(int userId, CreateGoalDto dto)
    {
        if (dto.CategoryId.HasValue)
        {
            var category = await _categoryRepository.GetByIdAsync(dto.CategoryId.Value);
            if (category == null || category.UserId != userId)
                throw new Exception("Category not found");

            var existingGoal = await _goalRepository.GetByUserIdAndCategoryAsync(userId, dto.CategoryId.Value);
            if (existingGoal != null)
                throw new Exception("An active goal already exists for this category");
        }

        var goal = _mapper.Map<Goal>(dto);
        goal.UserId = userId;
        goal.Status = GoalStatus.Active;
        goal.CurrentAmount = 0;

        await _goalRepository.AddAsync(goal);
        return _mapper.Map<GoalDto>(goal);
    }

    public async Task<GoalDto> UpdateAsync(int userId, int goalId, UpdateGoalDto dto)
    {
        var goal = await _goalRepository.GetByIdAsync(goalId);
        if (goal == null || goal.UserId != userId)
            throw new Exception("Goal not found");

        _mapper.Map(dto, goal);
        await _goalRepository.UpdateAsync(goal);

        return _mapper.Map<GoalDto>(goal);
    }

    public async Task DeleteAsync(int userId, int goalId)
    {
        var goal = await _goalRepository.GetByIdAsync(goalId);
        if (goal == null || goal.UserId != userId)
            throw new Exception("Goal not found");

        await _goalRepository.DeleteAsync(goalId);
    }

    public async Task UpdateProgressAsync(int userId, int goalId, decimal amount)
    {
        var goal = await _goalRepository.GetByIdAsync(goalId);
        if (goal == null || goal.UserId != userId)
            throw new Exception("Goal not found");

        await _goalRepository.UpdateGoalProgressAsync(goalId, amount);
    }
} 
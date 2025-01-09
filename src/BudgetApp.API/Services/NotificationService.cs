namespace BudgetApp.API.Services;

using AutoMapper;
using BudgetApp.API.Data.Repositories;
using BudgetApp.API.DTOs.Notifications;
using BudgetApp.API.Models;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IGoalRepository _goalRepository;
    private readonly IDeviceService _deviceService;
    private readonly IPushNotificationService _pushNotificationService;
    private readonly IMapper _mapper;

    public NotificationService(
        INotificationRepository notificationRepository,
        ICategoryRepository categoryRepository,
        IGoalRepository goalRepository,
        IDeviceService deviceService,
        IPushNotificationService pushNotificationService,
        IMapper mapper)
    {
        _notificationRepository = notificationRepository;
        _categoryRepository = categoryRepository;
        _goalRepository = goalRepository;
        _deviceService = deviceService;
        _pushNotificationService = pushNotificationService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<NotificationDto>> GetAllAsync(int userId)
    {
        var notifications = await _notificationRepository.GetByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<NotificationDto>>(notifications);
    }

    public async Task<IEnumerable<NotificationDto>> GetUnreadAsync(int userId)
    {
        var notifications = await _notificationRepository.GetUnreadByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<NotificationDto>>(notifications);
    }

    public async Task<NotificationDto> CreateAsync(int userId, CreateNotificationDto dto)
    {
        var notification = new Notification
        {
            UserId = userId,
            Title = dto.Title,
            Message = dto.Message,
            Type = dto.Type,
            IsRead = false
        };

        await _notificationRepository.AddAsync(notification);

        // Push notification gönder
        await _pushNotificationService.SendToUserAsync(userId, dto.Title, dto.Message);

        return _mapper.Map<NotificationDto>(notification);
    }

    public async Task MarkAsReadAsync(int userId, int notificationId)
    {
        var notification = await _notificationRepository.GetByIdAsync(notificationId);
        if (notification == null || notification.UserId != userId)
            throw new Exception("Notification not found");

        await _notificationRepository.MarkAsReadAsync(notificationId);
    }

    public async Task MarkAllAsReadAsync(int userId)
    {
        await _notificationRepository.MarkAllAsReadAsync(userId);
    }

    public async Task DeleteAsync(int userId, int notificationId)
    {
        var notification = await _notificationRepository.GetByIdAsync(notificationId);
        if (notification == null || notification.UserId != userId)
            throw new Exception("Notification not found");

        await _notificationRepository.DeleteAsync(notificationId);
    }

    public async Task NotifyBudgetExceededAsync(int userId, int categoryId, decimal amount, decimal limit)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);
        var message = $"Budget exceeded for {category.Name}. Current spending: {amount:C}, Limit: {limit:C}";

        await CreateAsync(userId, new CreateNotificationDto
        {
            Title = "Budget Alert",
            Message = message,
            Type = NotificationType.BudgetAlert
        });
    }

    public async Task NotifyGoalProgressAsync(int userId, int goalId, decimal progress)
    {
        var goal = await _goalRepository.GetByIdAsync(goalId);
        var message = $"You've reached {progress:N0}% of your goal: {goal.Title}";

        await CreateAsync(userId, new CreateNotificationDto
        {
            Title = "Goal Progress",
            Message = message,
            Type = NotificationType.GoalProgress
        });
    }

    public async Task NotifyGoalAchievedAsync(int userId, int goalId)
    {
        var goal = await _goalRepository.GetByIdAsync(goalId);
        var message = $"Congratulations! You've achieved your goal: {goal.Title}";

        await CreateAsync(userId, new CreateNotificationDto
        {
            Title = "Goal Achieved!",
            Message = message,
            Type = NotificationType.GoalAchieved
        });
    }

    public async Task NotifyLargeTransactionAsync(int userId, decimal amount, string categoryName)
    {
        var message = $"Large transaction detected: {amount:C} in {categoryName}";

        await CreateAsync(userId, new CreateNotificationDto
        {
            Title = "Large Transaction Alert",
            Message = message,
            Type = NotificationType.TransactionAlert
        });
    }
} 
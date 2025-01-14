namespace BudgetApp.API.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BudgetApp.API.Services;
using BudgetApp.API.DTOs.Notifications;
using System.Security.Claims;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    private int GetUserId()
    {
        return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<NotificationDto>>> GetAll()
    {
        var userId = GetUserId();
        var notifications = await _notificationService.GetAllAsync(userId);
        return Ok(notifications);
    }

    [HttpGet("unread")]
    public async Task<ActionResult<IEnumerable<NotificationDto>>> GetUnread()
    {
        var userId = GetUserId();
        var notifications = await _notificationService.GetUnreadAsync(userId);
        return Ok(notifications);
    }

    [HttpPost("{id}/read")]
    public async Task<ActionResult> MarkAsRead(int id)
    {
        try
        {
            var userId = GetUserId();
            await _notificationService.MarkAsReadAsync(userId, id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("read-all")]
    public async Task<ActionResult> MarkAllAsRead()
    {
        var userId = GetUserId();
        await _notificationService.MarkAllAsReadAsync(userId);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var userId = GetUserId();
            await _notificationService.DeleteAsync(userId, id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
} 
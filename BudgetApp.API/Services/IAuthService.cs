namespace BudgetApp.API.Services;

using BudgetApp.API.DTOs.Auth;

public interface IAuthService
{
    Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
    Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
    Task<string> GenerateJwtTokenAsync(Models.User user);
} 
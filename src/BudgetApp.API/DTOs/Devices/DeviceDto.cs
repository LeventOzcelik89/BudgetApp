namespace BudgetApp.API.DTOs.Devices;

public class DeviceDto
{
    public int Id { get; set; }
    public string DeviceToken { get; set; }
    public string DeviceType { get; set; }
    public bool IsActive { get; set; }
    public DateTime LastUsedAt { get; set; }
}

public class RegisterDeviceDto
{
    public string DeviceToken { get; set; }
    public string DeviceType { get; set; } // iOS, Android, Web
}

public class UpdateDeviceDto
{
    public string DeviceToken { get; set; }
    public bool IsActive { get; set; }
} 
namespace SeizureTrackerBlazer.Models;

public class UserInfoResponse
{
    // 'required' ensures these are never null when the object is created
    public required string UserId { get; set; }
    public required string Email { get; set; }
    public bool IsEmailConfirmed { get; set; }
    
    // Initialized to empty lists to avoid null reference exceptions in Razor components
    public List<string> Roles { get; set; } = [];
    public Dictionary<string, string> Claims { get; set; } = [];
    
    // 2026 Best Practice: Include a flag for Biometrics status
    public bool HasPasskeys { get; set; }
}
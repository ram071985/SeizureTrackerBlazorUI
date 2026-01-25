using System.ComponentModel.DataAnnotations;

namespace SeizureTrackerBlazer.Models;

public class RegisterModel
{
    [Required][EmailAddress] public string Email { get; set; } = "";
    [Required][MinLength(8)] public string Password { get; set; } = "";
}
using System.ComponentModel.DataAnnotations;

namespace StarItFront.Models.Registration;

public class RegistrationModel
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Nickname is required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Nickname must be between 3 and 50 characters")]
    public string Nickname { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password confirmation is required")]
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
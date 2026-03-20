using System.ComponentModel.DataAnnotations;
using VeterinaryClinic.Shared.Enums;

namespace VeterinaryClinic.Services.DTOs;

public class RegisterUserDto
{
    [Required(ErrorMessage = "Потребителското име е задължително")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Потребителското име трябва да е между 3 и 50 символа")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Паролата е задължителна")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Паролата трябва да е поне 6 символа")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Потвърждението на паролата е задължително")]
    [Compare(nameof(Password), ErrorMessage = "Паролите не съвпадат")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Имейлът е задължителен")]
    [EmailAddress(ErrorMessage = "Невалиден имейл адрес")]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Името е задължително")]
    [StringLength(50, ErrorMessage = "Името не може да надвишава 50 символа")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Фамилията е задължителна")]
    [StringLength(50, ErrorMessage = "Фамилията не може да надвишава 50 символа")]
    public string LastName { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Невалиден телефонен номер")]
    [StringLength(20)]
    public string? Phone { get; set; }

    public UserRole Role { get; set; }
}

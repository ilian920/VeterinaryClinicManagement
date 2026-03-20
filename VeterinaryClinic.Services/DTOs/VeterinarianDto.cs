using System.ComponentModel.DataAnnotations;

namespace VeterinaryClinic.Services.DTOs;

public class VeterinarianDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Името е задължително")]
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Фамилията е задължителна")]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    public string FullName => $"{FirstName} {LastName}";

    [Required(ErrorMessage = "Специализацията е задължителна")]
    [StringLength(100)]
    public string Specialization { get; set; } = string.Empty;

    [Required(ErrorMessage = "Телефонът е задължителен")]
    [Phone]
    [StringLength(20)]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Имейлът е задължителен")]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    [StringLength(500)]
    public string Bio { get; set; } = string.Empty;

    [StringLength(200)]
    public string? ImageUrl { get; set; }

    public bool IsActive { get; set; }
}

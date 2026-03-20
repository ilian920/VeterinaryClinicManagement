using System.ComponentModel.DataAnnotations;

namespace VeterinaryClinic.Services.DTOs;

public class VetServiceDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Наименованието е задължително")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Цената е задължителна")]
    [Range(0.01, 100000, ErrorMessage = "Цената трябва да е между 0.01 и 100000 лв.")]
    public decimal Price { get; set; }

    [Required]
    [Range(5, 480, ErrorMessage = "Продължителността трябва да е между 5 и 480 минути")]
    public int DurationMinutes { get; set; }

    public bool IsActive { get; set; }
}

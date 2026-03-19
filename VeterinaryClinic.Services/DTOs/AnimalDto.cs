using VeterinaryClinic.Shared.Enums;

namespace VeterinaryClinic.Services.DTOs;

public class AnimalDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Species { get; set; } = string.Empty;
    public string? Breed { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public AnimalGender Gender { get; set; }
    public decimal? Weight { get; set; }
    public string? Color { get; set; }
    public string? Notes { get; set; }
    public int OwnerId { get; set; }
    public string OwnerName { get; set; } = string.Empty;
}

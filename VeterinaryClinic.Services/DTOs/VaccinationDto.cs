namespace VeterinaryClinic.Services.DTOs;

public class VaccinationDto
{
    public int Id { get; set; }
    public int AnimalId { get; set; }
    public string AnimalName { get; set; } = string.Empty;
    public string VaccineName { get; set; } = string.Empty;
    public DateTime VaccineDate { get; set; }
    public DateTime? NextDueDate { get; set; }
    public int? VeterinarianId { get; set; }
    public string VeterinarianName { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public string? BatchNumber { get; set; }
}

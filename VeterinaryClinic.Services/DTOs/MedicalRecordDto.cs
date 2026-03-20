namespace VeterinaryClinic.Services.DTOs;

public class MedicalRecordDto
{
    public int Id { get; set; }
    public int AnimalId { get; set; }
    public string AnimalName { get; set; } = string.Empty;
    public int VeterinarianId { get; set; }
    public string VeterinarianName { get; set; } = string.Empty;
    public DateTime RecordDate { get; set; }
    public string Diagnosis { get; set; } = string.Empty;
    public string Treatment { get; set; } = string.Empty;
    public string? Prescription { get; set; }
    public string? Notes { get; set; }
    public DateTime? FollowUpDate { get; set; }
}

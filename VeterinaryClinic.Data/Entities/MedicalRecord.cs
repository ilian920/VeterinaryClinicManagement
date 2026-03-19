namespace VeterinaryClinic.Data.Entities;

public class MedicalRecord
{
    public int Id { get; set; }
    public int AnimalId { get; set; }
    public int VeterinarianId { get; set; }
    public DateTime RecordDate { get; set; }
    public string Diagnosis { get; set; } = string.Empty;
    public string Treatment { get; set; } = string.Empty;
    public string? Prescription { get; set; }
    public string? Notes { get; set; }
    public DateTime? FollowUpDate { get; set; }
    public DateTime CreatedAt { get; set; }

    public Animal Animal { get; set; } = null!;
    public Veterinarian Veterinarian { get; set; } = null!;
}

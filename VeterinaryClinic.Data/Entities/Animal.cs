using VeterinaryClinic.Shared.Enums;

namespace VeterinaryClinic.Data.Entities;

public class Animal
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

    public User Owner { get; set; } = null!;
    public ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
    public ICollection<Vaccination> Vaccinations { get; set; } = new List<Vaccination>();
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}

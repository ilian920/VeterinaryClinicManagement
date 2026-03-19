namespace VeterinaryClinic.Data.Entities;

public class Vaccination
{
    public int Id { get; set; }
    public int AnimalId { get; set; }
    public string VaccineName { get; set; } = string.Empty;
    public DateTime VaccineDate { get; set; }
    public DateTime? NextDueDate { get; set; }
    public int? VeterinarianId { get; set; }
    public string? Notes { get; set; }
    public string? BatchNumber { get; set; }
    public DateTime CreatedAt { get; set; }

    public Animal Animal { get; set; } = null!;
    public Veterinarian? Veterinarian { get; set; }
}

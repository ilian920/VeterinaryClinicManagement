using VeterinaryClinic.Shared.Enums;

namespace VeterinaryClinic.Data.Entities;

public class Appointment
{
    public int Id { get; set; }
    public DateTime AppointmentDate { get; set; }
    public AppointmentStatus Status { get; set; }
    public string? Notes { get; set; }
    public int OwnerId { get; set; }
    public int AnimalId { get; set; }
    public int VeterinarianId { get; set; }
    public int? ServiceId { get; set; }
    public DateTime CreatedAt { get; set; }

    public User Owner { get; set; } = null!;
    public Animal Animal { get; set; } = null!;
    public Veterinarian Veterinarian { get; set; } = null!;
    public VetService? Service { get; set; }
}

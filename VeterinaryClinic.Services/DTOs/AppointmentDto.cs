using VeterinaryClinic.Shared.Enums;

namespace VeterinaryClinic.Services.DTOs;

public class AppointmentDto
{
    public int Id { get; set; }
    public DateTime AppointmentDate { get; set; }
    public AppointmentStatus Status { get; set; }
    public string? Notes { get; set; }
    public int OwnerId { get; set; }
    public string OwnerName { get; set; } = string.Empty;
    public int AnimalId { get; set; }
    public string AnimalName { get; set; } = string.Empty;
    public int VeterinarianId { get; set; }
    public string VeterinarianName { get; set; } = string.Empty;
    public int? ServiceId { get; set; }
    public string ServiceName { get; set; } = string.Empty;
    public decimal? ServicePrice { get; set; }
    public DateTime CreatedAt { get; set; }
}

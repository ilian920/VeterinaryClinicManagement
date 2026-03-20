using VeterinaryClinic.Shared.Enums;

namespace VeterinaryClinic.Data.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public UserRole Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<Animal> Animals { get; set; } = new List<Animal>();
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}

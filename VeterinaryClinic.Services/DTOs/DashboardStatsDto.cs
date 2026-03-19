namespace VeterinaryClinic.Services.DTOs;

public class DashboardStatsDto
{
    public int TotalVeterinarians { get; set; }
    public int TotalAnimals { get; set; }
    public int TotalAppointments { get; set; }
    public int TotalOwners { get; set; }
    public int ScheduledAppointments { get; set; }
    public int TotalServices { get; set; }
}

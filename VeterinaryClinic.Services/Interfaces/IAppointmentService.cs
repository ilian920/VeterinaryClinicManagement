using VeterinaryClinic.Services.DTOs;

namespace VeterinaryClinic.Services.Interfaces;

public interface IAppointmentService
{
    Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync();
    Task<AppointmentDto?> GetAppointmentByIdAsync(int id);
    Task<IEnumerable<AppointmentDto>> GetAppointmentsByOwnerIdAsync(int ownerId);
    Task<IEnumerable<AppointmentDto>> GetAppointmentsByVeterinarianIdAsync(int vetId);
    Task<int> CreateAppointmentAsync(AppointmentDto dto);
    Task<bool> UpdateAppointmentAsync(AppointmentDto dto);
    Task<bool> CancelAppointmentAsync(int id);
    Task<DashboardStatsDto> GetStatisticsAsync();
}

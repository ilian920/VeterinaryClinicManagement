using VeterinaryClinic.Data.Entities;

namespace VeterinaryClinic.Data.Repositories;

public interface IAppointmentRepository : IRepository<Appointment>
{
    Task<IEnumerable<Appointment>> GetByOwnerIdAsync(int ownerId);
    Task<IEnumerable<Appointment>> GetByVeterinarianIdAsync(int veterinarianId);
    Task<IEnumerable<Appointment>> GetByDateAsync(DateTime date);
    Task<Appointment?> GetWithDetailsAsync(int id);
}

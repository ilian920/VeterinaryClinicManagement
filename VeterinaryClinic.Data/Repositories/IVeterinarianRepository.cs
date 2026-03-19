using VeterinaryClinic.Data.Entities;

namespace VeterinaryClinic.Data.Repositories;

public interface IVeterinarianRepository : IRepository<Veterinarian>
{
    Task<IEnumerable<Veterinarian>> GetAllActiveAsync();
    Task<Veterinarian?> GetWithAppointmentsAsync(int id);
}

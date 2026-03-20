using VeterinaryClinic.Data.Entities;

namespace VeterinaryClinic.Data.Repositories;

public interface IAnimalRepository : IRepository<Animal>
{
    Task<IEnumerable<Animal>> GetByOwnerIdAsync(int ownerId);
    Task<Animal?> GetWithMedicalHistoryAsync(int id);
}

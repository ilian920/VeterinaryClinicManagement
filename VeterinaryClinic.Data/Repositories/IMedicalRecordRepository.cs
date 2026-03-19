using VeterinaryClinic.Data.Entities;

namespace VeterinaryClinic.Data.Repositories;

public interface IMedicalRecordRepository : IRepository<MedicalRecord>
{
    Task<IEnumerable<MedicalRecord>> GetByAnimalIdAsync(int animalId);
    Task<MedicalRecord?> GetWithDetailsAsync(int id);
}

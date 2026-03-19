using VeterinaryClinic.Data.Entities;

namespace VeterinaryClinic.Data.Repositories;

public interface IVaccinationRepository : IRepository<Vaccination>
{
    Task<IEnumerable<Vaccination>> GetByAnimalIdAsync(int animalId);
    Task<IEnumerable<Vaccination>> GetOverdueAsync();
}

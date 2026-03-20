using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Data.Entities;

namespace VeterinaryClinic.Data.Repositories;

public class VaccinationRepository : Repository<Vaccination>, IVaccinationRepository
{
    public VaccinationRepository(VetClinicDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Vaccination>> GetByAnimalIdAsync(int animalId)
    {
        return await _dbSet
            .Include(v => v.Animal)
            .Include(v => v.Veterinarian)
            .Where(v => v.AnimalId == animalId)
            .OrderByDescending(v => v.VaccineDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Vaccination>> GetOverdueAsync()
    {
        var today = DateTime.Today;
        return await _dbSet
            .Include(v => v.Animal)
            .Include(v => v.Veterinarian)
            .Where(v => v.NextDueDate.HasValue && v.NextDueDate.Value < today)
            .ToListAsync();
    }
}

using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Data.Entities;

namespace VeterinaryClinic.Data.Repositories;

public class AnimalRepository : Repository<Animal>, IAnimalRepository
{
    public AnimalRepository(VetClinicDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Animal>> GetByOwnerIdAsync(int ownerId)
    {
        return await _dbSet.Where(a => a.OwnerId == ownerId).ToListAsync();
    }

    public async Task<Animal?> GetWithMedicalHistoryAsync(int id)
    {
        return await _dbSet
            .Include(a => a.MedicalRecords)
            .Include(a => a.Vaccinations)
            .Include(a => a.Owner)
            .FirstOrDefaultAsync(a => a.Id == id);
    }
}

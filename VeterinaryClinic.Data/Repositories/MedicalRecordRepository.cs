using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Data.Entities;

namespace VeterinaryClinic.Data.Repositories;

public class MedicalRecordRepository : Repository<MedicalRecord>, IMedicalRecordRepository
{
    public MedicalRecordRepository(VetClinicDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<MedicalRecord>> GetByAnimalIdAsync(int animalId)
    {
        return await _dbSet
            .Include(m => m.Animal)
            .Include(m => m.Veterinarian)
            .Where(m => m.AnimalId == animalId)
            .OrderByDescending(m => m.RecordDate)
            .ToListAsync();
    }

    public async Task<MedicalRecord?> GetWithDetailsAsync(int id)
    {
        return await _dbSet
            .Include(m => m.Animal)
            .Include(m => m.Veterinarian)
            .FirstOrDefaultAsync(m => m.Id == id);
    }
}

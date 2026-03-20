using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Data.Entities;

namespace VeterinaryClinic.Data.Repositories;

public class VeterinarianRepository : Repository<Veterinarian>, IVeterinarianRepository
{
    public VeterinarianRepository(VetClinicDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Veterinarian>> GetAllActiveAsync()
    {
        return await _dbSet.Where(v => v.IsActive).ToListAsync();
    }

    public async Task<Veterinarian?> GetWithAppointmentsAsync(int id)
    {
        return await _dbSet
            .Include(v => v.Appointments)
            .FirstOrDefaultAsync(v => v.Id == id);
    }
}

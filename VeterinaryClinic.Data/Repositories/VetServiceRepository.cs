using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Data.Entities;

namespace VeterinaryClinic.Data.Repositories;

public class VetServiceRepository : Repository<VetService>, IVetServiceRepository
{
    public VetServiceRepository(VetClinicDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<VetService>> GetAllActiveAsync()
    {
        return await _dbSet.Where(s => s.IsActive).ToListAsync();
    }
}

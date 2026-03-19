using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Data.Entities;

namespace VeterinaryClinic.Data.Repositories;

public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
{
    public AppointmentRepository(VetClinicDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Appointment>> GetByOwnerIdAsync(int ownerId)
    {
        return await _dbSet
            .Include(a => a.Animal)
            .Include(a => a.Veterinarian)
            .Include(a => a.Service)
            .Where(a => a.OwnerId == ownerId)
            .OrderByDescending(a => a.AppointmentDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Appointment>> GetByVeterinarianIdAsync(int veterinarianId)
    {
        return await _dbSet
            .Include(a => a.Animal)
            .Include(a => a.Owner)
            .Include(a => a.Service)
            .Where(a => a.VeterinarianId == veterinarianId)
            .OrderByDescending(a => a.AppointmentDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Appointment>> GetByDateAsync(DateTime date)
    {
        return await _dbSet
            .Include(a => a.Animal)
            .Include(a => a.Owner)
            .Include(a => a.Veterinarian)
            .Include(a => a.Service)
            .Where(a => a.AppointmentDate.Date == date.Date)
            .OrderBy(a => a.AppointmentDate)
            .ToListAsync();
    }

    public async Task<Appointment?> GetWithDetailsAsync(int id)
    {
        return await _dbSet
            .Include(a => a.Animal)
            .Include(a => a.Owner)
            .Include(a => a.Veterinarian)
            .Include(a => a.Service)
            .FirstOrDefaultAsync(a => a.Id == id);
    }
}

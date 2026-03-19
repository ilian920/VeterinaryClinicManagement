using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Data.Entities;

namespace VeterinaryClinic.Data.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(VetClinicDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetOwnerWithAnimalsAsync(int ownerId)
    {
        return await _dbSet
            .Include(u => u.Animals)
            .FirstOrDefaultAsync(u => u.Id == ownerId);
    }
}

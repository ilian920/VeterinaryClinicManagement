using VeterinaryClinic.Data.Entities;

namespace VeterinaryClinic.Data.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetOwnerWithAnimalsAsync(int ownerId);
}

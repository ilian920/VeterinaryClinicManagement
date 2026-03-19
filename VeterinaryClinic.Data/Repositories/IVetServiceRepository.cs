using VeterinaryClinic.Data.Entities;

namespace VeterinaryClinic.Data.Repositories;

public interface IVetServiceRepository : IRepository<VetService>
{
    Task<IEnumerable<VetService>> GetAllActiveAsync();
}

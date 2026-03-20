using VeterinaryClinic.Services.DTOs;

namespace VeterinaryClinic.Services.Interfaces;

public interface IVetServiceService
{
    Task<IEnumerable<VetServiceDto>> GetAllServicesAsync();
    Task<IEnumerable<VetServiceDto>> GetActiveServicesAsync();
    Task<VetServiceDto?> GetServiceByIdAsync(int id);
    Task<int> CreateServiceAsync(VetServiceDto dto);
    Task<bool> UpdateServiceAsync(VetServiceDto dto);
    Task<bool> DeleteServiceAsync(int id);
}

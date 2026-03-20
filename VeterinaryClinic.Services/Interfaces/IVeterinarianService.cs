using VeterinaryClinic.Services.DTOs;

namespace VeterinaryClinic.Services.Interfaces;

public interface IVeterinarianService
{
    Task<IEnumerable<VeterinarianDto>> GetAllVeterinariansAsync();
    Task<IEnumerable<VeterinarianDto>> GetActiveVeterinariansAsync();
    Task<VeterinarianDto?> GetVeterinarianByIdAsync(int id);
    Task<int> CreateVeterinarianAsync(VeterinarianDto dto);
    Task<bool> UpdateVeterinarianAsync(VeterinarianDto dto);
    Task<bool> DeleteVeterinarianAsync(int id);
}

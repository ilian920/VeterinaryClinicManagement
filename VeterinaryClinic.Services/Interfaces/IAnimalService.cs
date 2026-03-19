using VeterinaryClinic.Services.DTOs;

namespace VeterinaryClinic.Services.Interfaces;

public interface IAnimalService
{
    Task<IEnumerable<AnimalDto>> GetAnimalsByOwnerIdAsync(int ownerId);
    Task<AnimalDto?> GetAnimalByIdAsync(int id);
    Task<int> CreateAnimalAsync(AnimalDto dto);
    Task<bool> UpdateAnimalAsync(AnimalDto dto);
    Task<bool> DeleteAnimalAsync(int id);
}

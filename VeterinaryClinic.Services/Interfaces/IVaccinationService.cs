using VeterinaryClinic.Services.DTOs;

namespace VeterinaryClinic.Services.Interfaces;

public interface IVaccinationService
{
    Task<IEnumerable<VaccinationDto>> GetVaccinationsByAnimalIdAsync(int animalId);
    Task<VaccinationDto?> GetVaccinationByIdAsync(int id);
    Task<int> CreateVaccinationAsync(VaccinationDto dto);
    Task<bool> UpdateVaccinationAsync(VaccinationDto dto);
    Task<bool> DeleteVaccinationAsync(int id);
}

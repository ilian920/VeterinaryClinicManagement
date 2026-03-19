using VeterinaryClinic.Services.DTOs;

namespace VeterinaryClinic.Services.Interfaces;

public interface IMedicalRecordService
{
    Task<IEnumerable<MedicalRecordDto>> GetRecordsByAnimalIdAsync(int animalId);
    Task<MedicalRecordDto?> GetRecordByIdAsync(int id);
    Task<int> CreateRecordAsync(MedicalRecordDto dto);
    Task<bool> UpdateRecordAsync(MedicalRecordDto dto);
    Task<bool> DeleteRecordAsync(int id);
}

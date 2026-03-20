using AutoMapper;
using VeterinaryClinic.Data.Entities;
using VeterinaryClinic.Data.Repositories;
using VeterinaryClinic.Services.DTOs;
using VeterinaryClinic.Services.Interfaces;

namespace VeterinaryClinic.Services.Implementations;

public class VaccinationService : IVaccinationService
{
    private readonly IVaccinationRepository _vaccinationRepository;
    private readonly IMapper _mapper;

    public VaccinationService(IVaccinationRepository vaccinationRepository, IMapper mapper)
    {
        _vaccinationRepository = vaccinationRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<VaccinationDto>> GetVaccinationsByAnimalIdAsync(int animalId)
    {
        var vaccinations = await _vaccinationRepository.GetByAnimalIdAsync(animalId);
        return _mapper.Map<IEnumerable<VaccinationDto>>(vaccinations);
    }

    public async Task<VaccinationDto?> GetVaccinationByIdAsync(int id)
    {
        var vaccination = await _vaccinationRepository.GetByIdAsync(id);
        return vaccination != null ? _mapper.Map<VaccinationDto>(vaccination) : null;
    }

    public async Task<int> CreateVaccinationAsync(VaccinationDto dto)
    {
        var vaccination = new Vaccination
        {
            AnimalId = dto.AnimalId,
            VaccineName = dto.VaccineName,
            VaccineDate = dto.VaccineDate,
            NextDueDate = dto.NextDueDate,
            VeterinarianId = dto.VeterinarianId,
            Notes = dto.Notes,
            BatchNumber = dto.BatchNumber,
            CreatedAt = DateTime.Now
        };

        await _vaccinationRepository.AddAsync(vaccination);
        await _vaccinationRepository.SaveChangesAsync();
        return vaccination.Id;
    }

    public async Task<bool> UpdateVaccinationAsync(VaccinationDto dto)
    {
        var vaccination = await _vaccinationRepository.GetByIdAsync(dto.Id);
        if (vaccination == null)
        {
            return false;
        }

        vaccination.VaccineName = dto.VaccineName;
        vaccination.VaccineDate = dto.VaccineDate;
        vaccination.NextDueDate = dto.NextDueDate;
        vaccination.VeterinarianId = dto.VeterinarianId;
        vaccination.Notes = dto.Notes;
        vaccination.BatchNumber = dto.BatchNumber;

        await _vaccinationRepository.UpdateAsync(vaccination);
        await _vaccinationRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteVaccinationAsync(int id)
    {
        var vaccination = await _vaccinationRepository.GetByIdAsync(id);
        if (vaccination == null)
        {
            return false;
        }

        await _vaccinationRepository.DeleteAsync(vaccination);
        await _vaccinationRepository.SaveChangesAsync();

        return true;
    }
}

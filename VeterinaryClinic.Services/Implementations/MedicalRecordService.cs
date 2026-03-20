using AutoMapper;
using VeterinaryClinic.Data.Entities;
using VeterinaryClinic.Data.Repositories;
using VeterinaryClinic.Services.DTOs;
using VeterinaryClinic.Services.Interfaces;

namespace VeterinaryClinic.Services.Implementations;

public class MedicalRecordService : IMedicalRecordService
{
    private readonly IMedicalRecordRepository _medicalRecordRepository;
    private readonly IMapper _mapper;

    public MedicalRecordService(IMedicalRecordRepository medicalRecordRepository, IMapper mapper)
    {
        _medicalRecordRepository = medicalRecordRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MedicalRecordDto>> GetRecordsByAnimalIdAsync(int animalId)
    {
        var records = await _medicalRecordRepository.GetByAnimalIdAsync(animalId);
        return _mapper.Map<IEnumerable<MedicalRecordDto>>(records);
    }

    public async Task<MedicalRecordDto?> GetRecordByIdAsync(int id)
    {
        var record = await _medicalRecordRepository.GetWithDetailsAsync(id);
        return record != null ? _mapper.Map<MedicalRecordDto>(record) : null;
    }

    public async Task<int> CreateRecordAsync(MedicalRecordDto dto)
    {
        var record = new MedicalRecord
        {
            AnimalId = dto.AnimalId,
            VeterinarianId = dto.VeterinarianId,
            RecordDate = dto.RecordDate,
            Diagnosis = dto.Diagnosis,
            Treatment = dto.Treatment,
            Prescription = dto.Prescription,
            Notes = dto.Notes,
            FollowUpDate = dto.FollowUpDate,
            CreatedAt = DateTime.Now
        };

        await _medicalRecordRepository.AddAsync(record);
        await _medicalRecordRepository.SaveChangesAsync();
        return record.Id;
    }

    public async Task<bool> UpdateRecordAsync(MedicalRecordDto dto)
    {
        var record = await _medicalRecordRepository.GetByIdAsync(dto.Id);
        if (record == null)
        {
            return false;
        }

        record.RecordDate = dto.RecordDate;
        record.Diagnosis = dto.Diagnosis;
        record.Treatment = dto.Treatment;
        record.Prescription = dto.Prescription;
        record.Notes = dto.Notes;
        record.FollowUpDate = dto.FollowUpDate;

        await _medicalRecordRepository.UpdateAsync(record);
        await _medicalRecordRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteRecordAsync(int id)
    {
        var record = await _medicalRecordRepository.GetByIdAsync(id);
        if (record == null)
        {
            return false;
        }

        await _medicalRecordRepository.DeleteAsync(record);
        await _medicalRecordRepository.SaveChangesAsync();

        return true;
    }
}

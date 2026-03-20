using AutoMapper;
using VeterinaryClinic.Data.Entities;
using VeterinaryClinic.Data.Repositories;
using VeterinaryClinic.Services.DTOs;
using VeterinaryClinic.Services.Interfaces;

namespace VeterinaryClinic.Services.Implementations;

public class VeterinarianService : IVeterinarianService
{
    private readonly IVeterinarianRepository _veterinarianRepository;
    private readonly IMapper _mapper;

    public VeterinarianService(IVeterinarianRepository veterinarianRepository, IMapper mapper)
    {
        _veterinarianRepository = veterinarianRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<VeterinarianDto>> GetAllVeterinariansAsync()
    {
        var veterinarians = await _veterinarianRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<VeterinarianDto>>(veterinarians);
    }

    public async Task<IEnumerable<VeterinarianDto>> GetActiveVeterinariansAsync()
    {
        var veterinarians = await _veterinarianRepository.GetAllActiveAsync();
        return _mapper.Map<IEnumerable<VeterinarianDto>>(veterinarians);
    }

    public async Task<VeterinarianDto?> GetVeterinarianByIdAsync(int id)
    {
        var veterinarian = await _veterinarianRepository.GetByIdAsync(id);
        return veterinarian != null ? _mapper.Map<VeterinarianDto>(veterinarian) : null;
    }

    public async Task<int> CreateVeterinarianAsync(VeterinarianDto dto)
    {
        var veterinarian = _mapper.Map<Veterinarian>(dto);
        await _veterinarianRepository.AddAsync(veterinarian);
        await _veterinarianRepository.SaveChangesAsync();
        return veterinarian.Id;
    }

    public async Task<bool> UpdateVeterinarianAsync(VeterinarianDto dto)
    {
        var veterinarian = await _veterinarianRepository.GetByIdAsync(dto.Id);
        if (veterinarian == null)
        {
            return false;
        }

        veterinarian.FirstName = dto.FirstName;
        veterinarian.LastName = dto.LastName;
        veterinarian.Specialization = dto.Specialization;
        veterinarian.Phone = dto.Phone;
        veterinarian.Email = dto.Email;
        veterinarian.Bio = dto.Bio;
        veterinarian.ImageUrl = dto.ImageUrl;
        veterinarian.IsActive = dto.IsActive;

        await _veterinarianRepository.UpdateAsync(veterinarian);
        await _veterinarianRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteVeterinarianAsync(int id)
    {
        var veterinarian = await _veterinarianRepository.GetByIdAsync(id);
        if (veterinarian == null)
        {
            return false;
        }

        veterinarian.IsActive = false;
        await _veterinarianRepository.UpdateAsync(veterinarian);
        await _veterinarianRepository.SaveChangesAsync();

        return true;
    }
}

using AutoMapper;
using VeterinaryClinic.Data.Entities;
using VeterinaryClinic.Data.Repositories;
using VeterinaryClinic.Services.DTOs;
using VeterinaryClinic.Services.Interfaces;

namespace VeterinaryClinic.Services.Implementations;

public class VetServiceService : IVetServiceService
{
    private readonly IVetServiceRepository _vetServiceRepository;
    private readonly IMapper _mapper;

    public VetServiceService(IVetServiceRepository vetServiceRepository, IMapper mapper)
    {
        _vetServiceRepository = vetServiceRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<VetServiceDto>> GetAllServicesAsync()
    {
        var services = await _vetServiceRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<VetServiceDto>>(services);
    }

    public async Task<IEnumerable<VetServiceDto>> GetActiveServicesAsync()
    {
        var services = await _vetServiceRepository.GetAllActiveAsync();
        return _mapper.Map<IEnumerable<VetServiceDto>>(services);
    }

    public async Task<VetServiceDto?> GetServiceByIdAsync(int id)
    {
        var service = await _vetServiceRepository.GetByIdAsync(id);
        return service != null ? _mapper.Map<VetServiceDto>(service) : null;
    }

    public async Task<int> CreateServiceAsync(VetServiceDto dto)
    {
        var service = _mapper.Map<VetService>(dto);
        await _vetServiceRepository.AddAsync(service);
        await _vetServiceRepository.SaveChangesAsync();
        return service.Id;
    }

    public async Task<bool> UpdateServiceAsync(VetServiceDto dto)
    {
        var service = await _vetServiceRepository.GetByIdAsync(dto.Id);
        if (service == null)
        {
            return false;
        }

        service.Name = dto.Name;
        service.Description = dto.Description;
        service.Price = dto.Price;
        service.DurationMinutes = dto.DurationMinutes;
        service.IsActive = dto.IsActive;

        await _vetServiceRepository.UpdateAsync(service);
        await _vetServiceRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteServiceAsync(int id)
    {
        var service = await _vetServiceRepository.GetByIdAsync(id);
        if (service == null)
        {
            return false;
        }

        service.IsActive = false;
        await _vetServiceRepository.UpdateAsync(service);
        await _vetServiceRepository.SaveChangesAsync();

        return true;
    }
}

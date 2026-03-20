using AutoMapper;
using VeterinaryClinic.Data.Entities;
using VeterinaryClinic.Data.Repositories;
using VeterinaryClinic.Services.DTOs;
using VeterinaryClinic.Services.Interfaces;

namespace VeterinaryClinic.Services.Implementations;

public class AnimalService : IAnimalService
{
    private readonly IAnimalRepository _animalRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public AnimalService(IAnimalRepository animalRepository, IUserRepository userRepository, IMapper mapper)
    {
        _animalRepository = animalRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AnimalDto>> GetAnimalsByOwnerIdAsync(int ownerId)
    {
        var animals = await _animalRepository.GetByOwnerIdAsync(ownerId);
        var animalDtos = new List<AnimalDto>();
        
        foreach (var animal in animals)
        {
            var owner = await _userRepository.GetByIdAsync(animal.OwnerId);
            var dto = _mapper.Map<AnimalDto>(animal);
            if (owner != null)
            {
                dto.OwnerName = $"{owner.FirstName} {owner.LastName}";
            }
            animalDtos.Add(dto);
        }
        
        return animalDtos;
    }

    public async Task<AnimalDto?> GetAnimalByIdAsync(int id)
    {
        var animal = await _animalRepository.GetWithMedicalHistoryAsync(id);
        if (animal == null) return null;
        
        var dto = _mapper.Map<AnimalDto>(animal);
        return dto;
    }

    public async Task<int> CreateAnimalAsync(AnimalDto dto)
    {
        var animal = _mapper.Map<Animal>(dto);
        await _animalRepository.AddAsync(animal);
        await _animalRepository.SaveChangesAsync();
        return animal.Id;
    }

    public async Task<bool> UpdateAnimalAsync(AnimalDto dto)
    {
        var animal = await _animalRepository.GetByIdAsync(dto.Id);
        if (animal == null)
        {
            return false;
        }

        animal.Name = dto.Name;
        animal.Species = dto.Species;
        animal.Breed = dto.Breed;
        animal.DateOfBirth = dto.DateOfBirth;
        animal.Gender = dto.Gender;
        animal.Weight = dto.Weight;
        animal.Color = dto.Color;
        animal.Notes = dto.Notes;

        await _animalRepository.UpdateAsync(animal);
        await _animalRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAnimalAsync(int id)
    {
        var animal = await _animalRepository.GetByIdAsync(id);
        if (animal == null)
        {
            return false;
        }

        await _animalRepository.DeleteAsync(animal);
        await _animalRepository.SaveChangesAsync();

        return true;
    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using VeterinaryClinic.Data;
using VeterinaryClinic.Data.Repositories;
using VeterinaryClinic.Services;
using VeterinaryClinic.Services.DTOs;
using VeterinaryClinic.Services.Implementations;

namespace VeterinaryClinic.Tests;

public class VeterinarianServiceTests : IDisposable
{
    private readonly VetClinicDbContext _context;
    private readonly IMapper _mapper;
    private readonly VeterinarianService _service;

    public VeterinarianServiceTests()
    {
        var options = new DbContextOptionsBuilder<VetClinicDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new VetClinicDbContext(options);

        var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>(), NullLoggerFactory.Instance);
        _mapper = config.CreateMapper();

        var repo = new VeterinarianRepository(_context);
        _service = new VeterinarianService(repo, _mapper);
    }

    public void Dispose() => _context.Dispose();

    [Fact]
    public async Task CreateVeterinarianAsync_AddsAndReturnsId()
    {
        var dto = new VeterinarianDto
        {
            FirstName = "Иван",
            LastName = "Петров",
            Specialization = "Хирургия",
            Phone = "0888000000",
            Email = "ivan@vet.bg",
            IsActive = true
        };

        var id = await _service.CreateVeterinarianAsync(dto);

        Assert.True(id > 0);
        Assert.Equal(1, _context.Veterinarians.Count());
    }

    [Fact]
    public async Task GetActiveVeterinariansAsync_ReturnsOnlyActive()
    {
        await _service.CreateVeterinarianAsync(new VeterinarianDto { FirstName = "А", LastName = "А", Specialization = "S", Phone = "1", Email = "a@a.bg", IsActive = true });
        await _service.CreateVeterinarianAsync(new VeterinarianDto { FirstName = "Б", LastName = "Б", Specialization = "S", Phone = "2", Email = "b@b.bg", IsActive = false });

        var active = await _service.GetActiveVeterinariansAsync();

        Assert.Single(active);
    }

    [Fact]
    public async Task DeleteVeterinarianAsync_SetsIsActiveFalse()
    {
        var id = await _service.CreateVeterinarianAsync(new VeterinarianDto { FirstName = "В", LastName = "В", Specialization = "S", Phone = "3", Email = "v@v.bg", IsActive = true });

        var result = await _service.DeleteVeterinarianAsync(id);

        Assert.True(result);
        var vet = await _context.Veterinarians.FindAsync(id);
        Assert.False(vet!.IsActive);
    }

    [Fact]
    public async Task UpdateVeterinarianAsync_ChangesSpecialization()
    {
        var id = await _service.CreateVeterinarianAsync(new VeterinarianDto { FirstName = "Г", LastName = "Г", Specialization = "Old", Phone = "4", Email = "g@g.bg", IsActive = true });

        var dto = new VeterinarianDto { Id = id, FirstName = "Г", LastName = "Г", Specialization = "New", Phone = "4", Email = "g@g.bg", IsActive = true };
        var result = await _service.UpdateVeterinarianAsync(dto);

        Assert.True(result);
        var vet = await _context.Veterinarians.FindAsync(id);
        Assert.Equal("New", vet!.Specialization);
    }
}


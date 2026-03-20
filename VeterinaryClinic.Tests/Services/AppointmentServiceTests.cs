using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using VeterinaryClinic.Data;
using VeterinaryClinic.Data.Entities;
using VeterinaryClinic.Data.Repositories;
using VeterinaryClinic.Services;
using VeterinaryClinic.Services.DTOs;
using VeterinaryClinic.Services.Helpers;
using VeterinaryClinic.Services.Implementations;
using VeterinaryClinic.Shared.Enums;

namespace VeterinaryClinic.Tests.Services;

public class AppointmentServiceTests : IDisposable
{
    private readonly VetClinicDbContext _context;
    private readonly IMapper _mapper;

    public AppointmentServiceTests()
    {
        var options = new DbContextOptionsBuilder<VetClinicDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new VetClinicDbContext(options);

        var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>(), NullLoggerFactory.Instance);
        _mapper = config.CreateMapper();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    private async Task SeedTestDataAsync()
    {
        var owner = new User
        {
            Id = 1,
            Username = "testowner",
            PasswordHash = PasswordHelper.HashPassword("Test123!"),
            Email = "owner@test.bg",
            FirstName = "Тест",
            LastName = "Собственик",
            Role = UserRole.Owner,
            CreatedAt = DateTime.Now,
            IsActive = true
        };

        var vet = new Veterinarian
        {
            Id = 1,
            FirstName = "Иван",
            LastName = "Петров",
            Specialization = "Хирургия",
            Phone = "0888111222",
            Email = "ivan@test.bg",
            IsActive = true
        };

        var animal = new Animal
        {
            Id = 1,
            Name = "Макс",
            Species = "Куче",
            Gender = AnimalGender.Male,
            OwnerId = 1
        };

        _context.Users.Add(owner);
        _context.Veterinarians.Add(vet);
        _context.Animals.Add(animal);
        await _context.SaveChangesAsync();
    }

    [Fact]
    public async Task GetStatisticsAsync_ReturnsCorrectCounts()
    {
        // Arrange
        await SeedTestDataAsync();
        var appointmentRepo = new AppointmentRepository(_context);
        var service = new AppointmentService(appointmentRepo, _context, _mapper);

        // Act
        var stats = await service.GetStatisticsAsync();

        // Assert
        Assert.NotNull(stats);
        Assert.Equal(1, stats.TotalVeterinarians);
        Assert.Equal(1, stats.TotalAnimals);
        Assert.Equal(1, stats.TotalOwners);
    }

    [Fact]
    public async Task CreateAppointmentAsync_AddsAppointmentToDatabase()
    {
        // Arrange
        await SeedTestDataAsync();
        var appointmentRepo = new AppointmentRepository(_context);
        var service = new AppointmentService(appointmentRepo, _context, _mapper);

        var dto = new AppointmentDto
        {
            AppointmentDate = DateTime.Now.AddDays(1),
            Status = AppointmentStatus.Scheduled,
            OwnerId = 1,
            AnimalId = 1,
            VeterinarianId = 1,
            Notes = "Тестов час",
            CreatedAt = DateTime.Now
        };

        // Act
        var id = await service.CreateAppointmentAsync(dto);

        // Assert
        Assert.True(id > 0);
        var saved = await _context.Appointments.FindAsync(id);
        Assert.NotNull(saved);
        Assert.Equal(AppointmentStatus.Scheduled, saved.Status);
    }

    [Fact]
    public async Task CancelAppointmentAsync_ChangesStatusToCancelled()
    {
        // Arrange
        await SeedTestDataAsync();
        var appointment = new Appointment
        {
            AppointmentDate = DateTime.Now.AddDays(1),
            Status = AppointmentStatus.Scheduled,
            OwnerId = 1,
            AnimalId = 1,
            VeterinarianId = 1,
            CreatedAt = DateTime.Now
        };
        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        var appointmentRepo = new AppointmentRepository(_context);
        var service = new AppointmentService(appointmentRepo, _context, _mapper);

        // Act
        var result = await service.CancelAppointmentAsync(appointment.Id);

        // Assert
        Assert.True(result);
        var updated = await _context.Appointments.FindAsync(appointment.Id);
        Assert.Equal(AppointmentStatus.Cancelled, updated!.Status);
    }

    [Fact]
    public async Task GetAppointmentsByOwnerIdAsync_ReturnsOnlyOwnerAppointments()
    {
        // Arrange
        await SeedTestDataAsync();

        // Add second owner
        var owner2 = new User
        {
            Id = 2,
            Username = "owner2",
            PasswordHash = PasswordHelper.HashPassword("Test123!"),
            Email = "owner2@test.bg",
            FirstName = "Втори",
            LastName = "Собственик",
            Role = UserRole.Owner,
            CreatedAt = DateTime.Now,
            IsActive = true
        };
        var animal2 = new Animal { Id = 2, Name = "Луна", Species = "Котка", Gender = AnimalGender.Female, OwnerId = 2 };
        _context.Users.Add(owner2);
        _context.Animals.Add(animal2);
        await _context.SaveChangesAsync();

        _context.Appointments.AddRange(
            new Appointment { AppointmentDate = DateTime.Now.AddDays(1), Status = AppointmentStatus.Scheduled, OwnerId = 1, AnimalId = 1, VeterinarianId = 1, CreatedAt = DateTime.Now },
            new Appointment { AppointmentDate = DateTime.Now.AddDays(2), Status = AppointmentStatus.Scheduled, OwnerId = 2, AnimalId = 2, VeterinarianId = 1, CreatedAt = DateTime.Now }
        );
        await _context.SaveChangesAsync();

        var appointmentRepo = new AppointmentRepository(_context);
        var service = new AppointmentService(appointmentRepo, _context, _mapper);

        // Act
        var owner1Appts = await service.GetAppointmentsByOwnerIdAsync(1);

        // Assert
        Assert.Single(owner1Appts);
    }
}

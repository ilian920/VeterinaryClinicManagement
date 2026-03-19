using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Data;
using VeterinaryClinic.Data.Entities;
using VeterinaryClinic.Data.Repositories;
using VeterinaryClinic.Services.DTOs;
using VeterinaryClinic.Services.Interfaces;
using VeterinaryClinic.Shared.Enums;

namespace VeterinaryClinic.Services.Implementations;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly VetClinicDbContext _context;
    private readonly IMapper _mapper;

    public AppointmentService(IAppointmentRepository appointmentRepository, VetClinicDbContext context, IMapper mapper)
    {
        _appointmentRepository = appointmentRepository;
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AppointmentDto>> GetAllAppointmentsAsync()
    {
        var appointments = await _context.Appointments
            .Include(a => a.Owner)
            .Include(a => a.Animal)
            .Include(a => a.Veterinarian)
            .Include(a => a.Service)
            .OrderByDescending(a => a.AppointmentDate)
            .ToListAsync();
        
        return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
    }

    public async Task<AppointmentDto?> GetAppointmentByIdAsync(int id)
    {
        var appointment = await _appointmentRepository.GetWithDetailsAsync(id);
        return appointment != null ? _mapper.Map<AppointmentDto>(appointment) : null;
    }

    public async Task<IEnumerable<AppointmentDto>> GetAppointmentsByOwnerIdAsync(int ownerId)
    {
        var appointments = await _appointmentRepository.GetByOwnerIdAsync(ownerId);
        return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
    }

    public async Task<IEnumerable<AppointmentDto>> GetAppointmentsByVeterinarianIdAsync(int vetId)
    {
        var appointments = await _appointmentRepository.GetByVeterinarianIdAsync(vetId);
        return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
    }

    public async Task<int> CreateAppointmentAsync(AppointmentDto dto)
    {
        var appointment = new Appointment
        {
            AppointmentDate = dto.AppointmentDate,
            Status = dto.Status,
            Notes = dto.Notes,
            OwnerId = dto.OwnerId,
            AnimalId = dto.AnimalId,
            VeterinarianId = dto.VeterinarianId,
            ServiceId = dto.ServiceId,
            CreatedAt = DateTime.Now
        };

        await _appointmentRepository.AddAsync(appointment);
        await _appointmentRepository.SaveChangesAsync();
        return appointment.Id;
    }

    public async Task<bool> UpdateAppointmentAsync(AppointmentDto dto)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(dto.Id);
        if (appointment == null)
        {
            return false;
        }

        appointment.AppointmentDate = dto.AppointmentDate;
        appointment.Status = dto.Status;
        appointment.Notes = dto.Notes;
        appointment.AnimalId = dto.AnimalId;
        appointment.VeterinarianId = dto.VeterinarianId;
        appointment.ServiceId = dto.ServiceId;

        await _appointmentRepository.UpdateAsync(appointment);
        await _appointmentRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> CancelAppointmentAsync(int id)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(id);
        if (appointment == null)
        {
            return false;
        }

        appointment.Status = AppointmentStatus.Cancelled;
        await _appointmentRepository.UpdateAsync(appointment);
        await _appointmentRepository.SaveChangesAsync();

        return true;
    }

    public async Task<DashboardStatsDto> GetStatisticsAsync()
    {
        var stats = new DashboardStatsDto
        {
            TotalVeterinarians = await _context.Veterinarians.CountAsync(v => v.IsActive),
            TotalAnimals = await _context.Animals.CountAsync(),
            TotalAppointments = await _context.Appointments.CountAsync(),
            TotalOwners = await _context.Users.CountAsync(u => u.Role == UserRole.Owner && u.IsActive),
            ScheduledAppointments = await _context.Appointments.CountAsync(a => a.Status == AppointmentStatus.Scheduled),
            TotalServices = await _context.VetServices.CountAsync(s => s.IsActive)
        };

        return stats;
    }
}

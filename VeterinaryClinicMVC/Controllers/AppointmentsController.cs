using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Services.DTOs;
using VeterinaryClinic.Services.Interfaces;
using VeterinaryClinic.Shared.Enums;

namespace VeterinaryClinicMVC.Controllers;

public class AppointmentsController : BaseController
{
    private readonly IAppointmentService _appointmentService;
    private readonly IVeterinarianService _veterinarianService;
    private readonly IVetServiceService _vetServiceService;
    private readonly IAnimalService _animalService;

    public AppointmentsController(
        IAppointmentService appointmentService,
        IVeterinarianService veterinarianService,
        IVetServiceService vetServiceService,
        IAnimalService animalService)
    {
        _appointmentService = appointmentService;
        _veterinarianService = veterinarianService;
        _vetServiceService = vetServiceService;
        _animalService = animalService;
    }

    public async Task<IActionResult> Index()
    {
        if (!CheckAuth() || !CurrentUserId.HasValue)
        {
            return RedirectToLogin();
        }

        var appointments = await _appointmentService.GetAppointmentsByOwnerIdAsync(CurrentUserId.Value);
        return View(appointments);
    }

    public async Task<IActionResult> Details(int id)
    {
        if (!CheckAuth())
        {
            return RedirectToLogin();
        }

        var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
        if (appointment == null)
        {
            return NotFound();
        }

        return View(appointment);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        if (!CheckAuth() || !CurrentUserId.HasValue)
        {
            return RedirectToLogin();
        }

        ViewBag.Veterinarians = await _veterinarianService.GetActiveVeterinariansAsync();
        ViewBag.Services = await _vetServiceService.GetActiveServicesAsync();
        ViewBag.Animals = await _animalService.GetAnimalsByOwnerIdAsync(CurrentUserId.Value);

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AppointmentDto model)
    {
        if (!CheckAuth() || !CurrentUserId.HasValue)
        {
            return RedirectToLogin();
        }

        model.OwnerId = CurrentUserId.Value;
        model.Status = AppointmentStatus.Scheduled;
        model.CreatedAt = DateTime.Now;

        await _appointmentService.CreateAppointmentAsync(model);
        TempData["Success"] = "Appointment created successfully!";
        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancel(int id)
    {
        if (!CheckAuth())
        {
            return RedirectToLogin();
        }

        await _appointmentService.CancelAppointmentAsync(id);
        TempData["Success"] = "Appointment cancelled successfully!";
        return RedirectToAction("Index");
    }
}

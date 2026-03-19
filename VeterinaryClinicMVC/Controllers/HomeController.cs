using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Services.DTOs;
using VeterinaryClinic.Services.Interfaces;
using VeterinaryClinic.Shared.Enums;
using VeterinaryClinicMVC.Models;

namespace VeterinaryClinicMVC.Controllers;

public class HomeController : BaseController
{
    private readonly IVeterinarianService _veterinarianService;
    private readonly IVetServiceService _vetServiceService;
    private readonly IAnimalService _animalService;
    private readonly IAppointmentService _appointmentService;

    public HomeController(
        IVeterinarianService veterinarianService,
        IVetServiceService vetServiceService,
        IAnimalService animalService,
        IAppointmentService appointmentService)
    {
        _veterinarianService = veterinarianService;
        _vetServiceService = vetServiceService;
        _animalService = animalService;
        _appointmentService = appointmentService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    public async Task<IActionResult> Team()
    {
        var veterinarians = await _veterinarianService.GetActiveVeterinariansAsync();
        return View(veterinarians);
    }

    public async Task<IActionResult> Services()
    {
        var services = await _vetServiceService.GetActiveServicesAsync();
        return View(services);
    }

    public IActionResult Contact()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> BookAppointment()
    {
        if (!CheckAuth())
        {
            return RedirectToLogin();
        }

        ViewBag.Veterinarians = await _veterinarianService.GetActiveVeterinariansAsync();
        ViewBag.Services = await _vetServiceService.GetActiveServicesAsync();
        
        if (IsOwner && CurrentUserId.HasValue)
        {
            ViewBag.Animals = await _animalService.GetAnimalsByOwnerIdAsync(CurrentUserId.Value);
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> BookAppointment(AppointmentDto model)
    {
        if (!CheckAuth() || !CurrentUserId.HasValue)
        {
            return RedirectToLogin();
        }

        model.OwnerId = CurrentUserId.Value;
        model.Status = AppointmentStatus.Scheduled;
        model.CreatedAt = DateTime.Now;

        await _appointmentService.CreateAppointmentAsync(model);
        TempData["Success"] = "Appointment booked successfully!";
        
        return RedirectToAction("Index", "Appointments");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

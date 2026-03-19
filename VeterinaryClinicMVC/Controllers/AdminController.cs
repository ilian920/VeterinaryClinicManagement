using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Services.DTOs;
using VeterinaryClinic.Services.Interfaces;
using VeterinaryClinic.Shared.Enums;

namespace VeterinaryClinicMVC.Controllers;

public class AdminController : BaseController
{
    private readonly IVeterinarianService _veterinarianService;
    private readonly IVetServiceService _vetServiceService;
    private readonly IAppointmentService _appointmentService;
    private readonly IUserService _userService;
    private readonly IAnimalService _animalService;

    public AdminController(
        IVeterinarianService veterinarianService,
        IVetServiceService vetServiceService,
        IAppointmentService appointmentService,
        IUserService userService,
        IAnimalService animalService)
    {
        _veterinarianService = veterinarianService;
        _vetServiceService = vetServiceService;
        _appointmentService = appointmentService;
        _userService = userService;
        _animalService = animalService;
    }

    public async Task<IActionResult> Dashboard()
    {
        if (!CheckAdminAuth())
        {
            return RedirectToLogin();
        }

        var stats = await _appointmentService.GetStatisticsAsync();
        return View(stats);
    }

    public async Task<IActionResult> Veterinarians()
    {
        if (!CheckAdminAuth())
        {
            return RedirectToLogin();
        }

        var veterinarians = await _veterinarianService.GetAllVeterinariansAsync();
        return View(veterinarians);
    }

    [HttpGet]
    public IActionResult CreateVeterinarian()
    {
        if (!CheckAdminAuth())
        {
            return RedirectToLogin();
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateVeterinarian(VeterinarianDto model)
    {
        if (!CheckAdminAuth())
        {
            return RedirectToLogin();
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        model.IsActive = true;
        await _veterinarianService.CreateVeterinarianAsync(model);
        TempData["Success"] = "Veterinarian created successfully!";
        return RedirectToAction("Veterinarians");
    }

    [HttpGet]
    public async Task<IActionResult> EditVeterinarian(int id)
    {
        if (!CheckAdminAuth())
        {
            return RedirectToLogin();
        }

        var vet = await _veterinarianService.GetVeterinarianByIdAsync(id);
        if (vet == null)
        {
            return NotFound();
        }

        return View(vet);
    }

    [HttpPost]
    public async Task<IActionResult> EditVeterinarian(VeterinarianDto model)
    {
        if (!CheckAdminAuth())
        {
            return RedirectToLogin();
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _veterinarianService.UpdateVeterinarianAsync(model);
        TempData["Success"] = "Veterinarian updated successfully!";
        return RedirectToAction("Veterinarians");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteVeterinarian(int id)
    {
        if (!CheckAdminAuth())
        {
            return RedirectToLogin();
        }

        await _veterinarianService.DeleteVeterinarianAsync(id);
        TempData["Success"] = "Veterinarian deleted successfully!";
        return RedirectToAction("Veterinarians");
    }

    public async Task<IActionResult> Services()
    {
        if (!CheckAdminAuth())
        {
            return RedirectToLogin();
        }

        var services = await _vetServiceService.GetAllServicesAsync();
        return View(services);
    }

    [HttpGet]
    public IActionResult CreateService()
    {
        if (!CheckAdminAuth())
        {
            return RedirectToLogin();
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateService(VetServiceDto model)
    {
        if (!CheckAdminAuth())
        {
            return RedirectToLogin();
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        model.IsActive = true;
        await _vetServiceService.CreateServiceAsync(model);
        TempData["Success"] = "Service created successfully!";
        return RedirectToAction("Services");
    }

    [HttpGet]
    public async Task<IActionResult> EditService(int id)
    {
        if (!CheckAdminAuth())
        {
            return RedirectToLogin();
        }

        var service = await _vetServiceService.GetServiceByIdAsync(id);
        if (service == null)
        {
            return NotFound();
        }

        return View(service);
    }

    [HttpPost]
    public async Task<IActionResult> EditService(VetServiceDto model)
    {
        if (!CheckAdminAuth())
        {
            return RedirectToLogin();
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _vetServiceService.UpdateServiceAsync(model);
        TempData["Success"] = "Service updated successfully!";
        return RedirectToAction("Services");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteService(int id)
    {
        if (!CheckAdminAuth())
        {
            return RedirectToLogin();
        }

        await _vetServiceService.DeleteServiceAsync(id);
        TempData["Success"] = "Service deleted successfully!";
        return RedirectToAction("Services");
    }

    public async Task<IActionResult> Appointments()
    {
        if (!CheckAdminAuth())
        {
            return RedirectToLogin();
        }

        var appointments = await _appointmentService.GetAllAppointmentsAsync();
        return View(appointments);
    }

    [HttpGet]
    public async Task<IActionResult> CreateAppointment()
    {
        if (!CheckAdminAuth())
        {
            return RedirectToLogin();
        }

        ViewBag.Veterinarians = await _veterinarianService.GetActiveVeterinariansAsync();
        ViewBag.Services = await _vetServiceService.GetActiveServicesAsync();
        ViewBag.Users = await _userService.GetAllUsersAsync();

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateAppointment(AppointmentDto model)
    {
        if (!CheckAdminAuth())
        {
            return RedirectToLogin();
        }

        model.Status = AppointmentStatus.Scheduled;
        model.CreatedAt = DateTime.Now;

        await _appointmentService.CreateAppointmentAsync(model);
        TempData["Success"] = "Appointment created successfully!";
        return RedirectToAction("Appointments");
    }

    [HttpGet]
    public async Task<IActionResult> EditAppointment(int id)
    {
        if (!CheckAdminAuth())
        {
            return RedirectToLogin();
        }

        var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
        if (appointment == null)
        {
            return NotFound();
        }

        ViewBag.Veterinarians = await _veterinarianService.GetActiveVeterinariansAsync();
        ViewBag.Services = await _vetServiceService.GetActiveServicesAsync();

        return View(appointment);
    }

    [HttpPost]
    public async Task<IActionResult> EditAppointment(AppointmentDto model)
    {
        if (!CheckAdminAuth())
        {
            return RedirectToLogin();
        }

        await _appointmentService.UpdateAppointmentAsync(model);
        TempData["Success"] = "Appointment updated successfully!";
        return RedirectToAction("Appointments");
    }

    [HttpPost]
    public async Task<IActionResult> CancelAppointment(int id)
    {
        if (!CheckAdminAuth())
        {
            return RedirectToLogin();
        }

        await _appointmentService.CancelAppointmentAsync(id);
        TempData["Success"] = "Appointment cancelled successfully!";
        return RedirectToAction("Appointments");
    }

    public async Task<IActionResult> Users()
    {
        if (!CheckAdminAuth())
        {
            return RedirectToLogin();
        }

        var users = await _userService.GetAllUsersAsync();
        return View(users);
    }
}

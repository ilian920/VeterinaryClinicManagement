using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Services.DTOs;
using VeterinaryClinic.Services.Interfaces;

namespace VeterinaryClinicMVC.Controllers;

public class VaccinationsController : BaseController
{
    private readonly IVaccinationService _vaccinationService;
    private readonly IVeterinarianService _veterinarianService;
    private readonly IAnimalService _animalService;

    public VaccinationsController(
        IVaccinationService vaccinationService,
        IVeterinarianService veterinarianService,
        IAnimalService animalService)
    {
        _vaccinationService = vaccinationService;
        _veterinarianService = veterinarianService;
        _animalService = animalService;
    }

    public async Task<IActionResult> Index(int animalId)
    {
        if (!CheckAuth())
        {
            return RedirectToLogin();
        }

        var vaccinations = await _vaccinationService.GetVaccinationsByAnimalIdAsync(animalId);
        ViewBag.AnimalId = animalId;
        var animal = await _animalService.GetAnimalByIdAsync(animalId);
        ViewBag.AnimalName = animal?.Name ?? "";

        return View(vaccinations);
    }

    [HttpGet]
    public async Task<IActionResult> Create(int animalId)
    {
        if (!CheckAuth())
        {
            return RedirectToLogin();
        }

        ViewBag.Veterinarians = await _veterinarianService.GetActiveVeterinariansAsync();
        ViewBag.AnimalId = animalId;
        var animal = await _animalService.GetAnimalByIdAsync(animalId);
        ViewBag.AnimalName = animal?.Name ?? "";

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(VaccinationDto model)
    {
        if (!CheckAuth())
        {
            return RedirectToLogin();
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Veterinarians = await _veterinarianService.GetActiveVeterinariansAsync();
            return View(model);
        }

        await _vaccinationService.CreateVaccinationAsync(model);
        TempData["Success"] = "Vaccination record created successfully!";
        return RedirectToAction("Details", "Animals", new { id = model.AnimalId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, int animalId)
    {
        if (!CheckAuth())
        {
            return RedirectToLogin();
        }

        await _vaccinationService.DeleteVaccinationAsync(id);
        TempData["Success"] = "Vaccination record deleted successfully!";
        return RedirectToAction("Details", "Animals", new { id = animalId });
    }
}

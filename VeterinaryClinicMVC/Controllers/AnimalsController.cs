using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Services.DTOs;
using VeterinaryClinic.Services.Interfaces;

namespace VeterinaryClinicMVC.Controllers;

public class AnimalsController : BaseController
{
    private readonly IAnimalService _animalService;
    private readonly IMedicalRecordService _medicalRecordService;
    private readonly IVaccinationService _vaccinationService;

    public AnimalsController(
        IAnimalService animalService,
        IMedicalRecordService medicalRecordService,
        IVaccinationService vaccinationService)
    {
        _animalService = animalService;
        _medicalRecordService = medicalRecordService;
        _vaccinationService = vaccinationService;
    }

    public async Task<IActionResult> Index()
    {
        if (!CheckAuth() || !CurrentUserId.HasValue)
        {
            return RedirectToLogin();
        }

        var animals = await _animalService.GetAnimalsByOwnerIdAsync(CurrentUserId.Value);
        return View(animals);
    }

    public async Task<IActionResult> Details(int id)
    {
        if (!CheckAuth())
        {
            return RedirectToLogin();
        }

        var animal = await _animalService.GetAnimalByIdAsync(id);
        if (animal == null)
        {
            return NotFound();
        }

        ViewBag.MedicalRecords = await _medicalRecordService.GetRecordsByAnimalIdAsync(id);
        ViewBag.Vaccinations = await _vaccinationService.GetVaccinationsByAnimalIdAsync(id);

        return View(animal);
    }

    [HttpGet]
    public IActionResult Create()
    {
        if (!CheckAuth())
        {
            return RedirectToLogin();
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(AnimalDto model)
    {
        if (!CheckAuth() || !CurrentUserId.HasValue)
        {
            return RedirectToLogin();
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        model.OwnerId = CurrentUserId.Value;
        await _animalService.CreateAnimalAsync(model);
        TempData["Success"] = "Animal added successfully!";
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        if (!CheckAuth())
        {
            return RedirectToLogin();
        }

        var animal = await _animalService.GetAnimalByIdAsync(id);
        if (animal == null)
        {
            return NotFound();
        }

        return View(animal);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(AnimalDto model)
    {
        if (!CheckAuth())
        {
            return RedirectToLogin();
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _animalService.UpdateAnimalAsync(model);
        TempData["Success"] = "Animal updated successfully!";
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        if (!CheckAuth())
        {
            return RedirectToLogin();
        }

        await _animalService.DeleteAnimalAsync(id);
        TempData["Success"] = "Animal deleted successfully!";
        return RedirectToAction("Index");
    }
}

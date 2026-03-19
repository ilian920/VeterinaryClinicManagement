using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Services.DTOs;
using VeterinaryClinic.Services.Interfaces;

namespace VeterinaryClinicMVC.Controllers;

public class MedicalRecordsController : BaseController
{
    private readonly IMedicalRecordService _medicalRecordService;
    private readonly IVeterinarianService _veterinarianService;
    private readonly IAnimalService _animalService;

    public MedicalRecordsController(
        IMedicalRecordService medicalRecordService,
        IVeterinarianService veterinarianService,
        IAnimalService animalService)
    {
        _medicalRecordService = medicalRecordService;
        _veterinarianService = veterinarianService;
        _animalService = animalService;
    }

    public async Task<IActionResult> Index(int animalId)
    {
        if (!CheckAuth())
        {
            return RedirectToLogin();
        }

        var records = await _medicalRecordService.GetRecordsByAnimalIdAsync(animalId);
        ViewBag.AnimalId = animalId;
        var animal = await _animalService.GetAnimalByIdAsync(animalId);
        ViewBag.AnimalName = animal?.Name ?? "";

        return View(records);
    }

    public async Task<IActionResult> Details(int id)
    {
        if (!CheckAuth())
        {
            return RedirectToLogin();
        }

        var record = await _medicalRecordService.GetRecordByIdAsync(id);
        if (record == null)
        {
            return NotFound();
        }

        return View(record);
    }

    [HttpGet]
    public async Task<IActionResult> Create(int animalId)
    {
        if (!CheckAdminAuth())
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
    public async Task<IActionResult> Create(MedicalRecordDto model)
    {
        if (!CheckAdminAuth())
        {
            return RedirectToLogin();
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Veterinarians = await _veterinarianService.GetActiveVeterinariansAsync();
            return View(model);
        }

        await _medicalRecordService.CreateRecordAsync(model);
        TempData["Success"] = "Medical record created successfully!";
        return RedirectToAction("Index", new { animalId = model.AnimalId });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        if (!CheckAdminAuth())
        {
            return RedirectToLogin();
        }

        var record = await _medicalRecordService.GetRecordByIdAsync(id);
        if (record == null)
        {
            return NotFound();
        }

        ViewBag.Veterinarians = await _veterinarianService.GetActiveVeterinariansAsync();

        return View(record);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(MedicalRecordDto model)
    {
        if (!CheckAdminAuth())
        {
            return RedirectToLogin();
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Veterinarians = await _veterinarianService.GetActiveVeterinariansAsync();
            return View(model);
        }

        await _medicalRecordService.UpdateRecordAsync(model);
        TempData["Success"] = "Medical record updated successfully!";
        return RedirectToAction("Index", new { animalId = model.AnimalId });
    }
}

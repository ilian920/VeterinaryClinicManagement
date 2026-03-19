using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Services.DTOs;
using VeterinaryClinic.Services.Interfaces;
using VeterinaryClinic.Shared.Enums;

namespace VeterinaryClinicMVC.Controllers;

public class AccountController : BaseController
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        if (IsAuthenticated)
        {
            return RedirectToHome();
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        var user = await _userService.ValidateLoginAsync(username, password);
        
        if (user == null)
        {
            ViewBag.Error = "Invalid username or password";
            return View();
        }

        HttpContext.Session.SetInt32("UserId", user.Id);
        HttpContext.Session.SetString("Username", user.Username);
        HttpContext.Session.SetInt32("UserRole", (int)user.Role);

        if (user.Role == UserRole.Admin)
        {
            return RedirectToAction("Dashboard", "Admin");
        }
        
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Register()
    {
        if (IsAuthenticated)
        {
            return RedirectToHome();
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterUserDto model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        model.Role = UserRole.Owner;
        var (success, message) = await _userService.RegisterUserAsync(model);

        if (!success)
        {
            ViewBag.Error = message;
            return View(model);
        }

        TempData["Success"] = "Registration successful! Please log in.";
        return RedirectToAction("Login");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        if (!CheckAuth() || !CurrentUserId.HasValue)
        {
            return RedirectToLogin();
        }

        var user = await _userService.GetUserByIdAsync(CurrentUserId.Value);
        if (user == null)
        {
            return NotFound();
        }

        return View(user);
    }

    private IActionResult RedirectToHome()
    {
        if (IsAdmin)
        {
            return RedirectToAction("Dashboard", "Admin");
        }
        return RedirectToAction("Index", "Home");
    }
}

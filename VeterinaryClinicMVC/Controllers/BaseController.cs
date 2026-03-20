using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using VeterinaryClinic.Shared.Enums;

namespace VeterinaryClinicMVC.Controllers;

public class BaseController : Controller
{
    protected int? CurrentUserId => HttpContext.Session.GetInt32("UserId");
    protected string? CurrentUsername => HttpContext.Session.GetString("Username");
    protected UserRole? CurrentUserRole
    {
        get
        {
            var role = HttpContext.Session.GetInt32("UserRole");
            return role.HasValue ? (UserRole)role.Value : null;
        }
    }

    protected bool IsAuthenticated => CurrentUserId.HasValue;
    protected bool IsAdmin => CurrentUserRole == UserRole.Admin;
    protected bool IsOwner => CurrentUserRole == UserRole.Owner;
    protected bool IsVeterinarian => CurrentUserRole == UserRole.Veterinarian;

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        ViewBag.CurrentUserId = CurrentUserId;
        ViewBag.CurrentUsername = CurrentUsername;
        ViewBag.CurrentUserRole = CurrentUserRole;
        ViewBag.IsAuthenticated = IsAuthenticated;
        
        base.OnActionExecuting(context);
    }

    protected IActionResult RedirectToLogin()
    {
        return RedirectToAction("Login", "Account");
    }

    protected bool CheckAuth()
    {
        return IsAuthenticated;
    }

    protected bool CheckAdminAuth()
    {
        return IsAuthenticated && IsAdmin;
    }
}

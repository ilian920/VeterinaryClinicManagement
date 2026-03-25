using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace VeterinaryClinicMVC.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public IActionResult Index()
        {
            // Sample logic to get statistics and reporting details
            var statistics = new
            {
                TotalAppointments = 150,
                TotalRevenue = 25000.50,
                TotalClients = 120,
            };

            return View(statistics);
        }

        // Sample method to view reports
        public IActionResult Reports()
        {
            // Logic to retrieve report details
            var reports = new
            {
                DailyVisits = 30,
                MonthlyRevenue = 5000.75,
            };

            return View(reports);
        }
    }
}
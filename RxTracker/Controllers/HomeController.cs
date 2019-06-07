using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RxTracker.Data;
using RxTracker.Models;
using System.Diagnostics;

namespace RxTracker.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(ApplicationDbContext context)
        {
            context.Database.Migrate();
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return Redirect("/Identity/Account/Login");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using RASCHET_HASHODOV.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;

namespace RASCHET_HASHODOV.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                return RedirectToAction("Index", "Expense");
            }

            return View();
        }
    }
}

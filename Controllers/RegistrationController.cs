using RASCHET_HASHODOV.IRepositories;
using RASCHET_HASHODOV.ViewModels.forUser;
using Microsoft.AspNetCore.Mvc;

namespace RASCHET_HASHODOV.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IUserRepository _userRepository;

        public RegistrationController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userRepository.RegisterUser(model);
                if (result)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Возможно, имя пользователя уже занято или указан неверный пароль");
            }
            return View(model);
        }
    }
}

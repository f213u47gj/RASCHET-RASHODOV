﻿using RASCHET_HASHODOV.IRepositories;
using RASCHET_HASHODOV.ViewModels.forUser;
using Microsoft.AspNetCore.Mvc;

namespace RASCHET_HASHODOV.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserRepository _userRepository;

        public LoginController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userRepository.LoginUser(model.UserName, model.Password, model.RememberMe);
                if (result)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Неправильное имя пользователя или пароль");
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _userRepository.Logout();
            return RedirectToAction("", "Expense");
        }
    }
}

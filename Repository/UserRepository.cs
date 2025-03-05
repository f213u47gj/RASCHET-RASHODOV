using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RASCHET_HASHODOV.Data;
using RASCHET_HASHODOV.IRepositories;
using RASCHET_HASHODOV.Models;
using RASCHET_HASHODOV.ViewModels.forUser;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RASCHET_HASHODOV.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<User> GetUserById(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<bool> ChangeUserPassword(string userId, string oldPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            return result.Succeeded;
        }

        public async Task<bool> LoginUser(string userName, string password, bool rememberMe)
        {
            var result = await _signInManager.PasswordSignInAsync(userName, password, rememberMe, false);
            return result.Succeeded;
        }

        public async Task<bool> RegisterUser(RegistrationViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Password))
            {
                return false;
            }

            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null) return false;

            var user = new User
            {
                Email = model.Email,
                UserName = model.UserName
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return false;
            }

            await AssignUserRole(user);
            await _signInManager.SignInAsync(user, isPersistent: false);
            return true;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        private async Task AssignUserRole(User user)
        {
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }

            var role = (user.Email == "nikitanik10305@gmail.com") ? "Admin" : "User";
            await _userManager.AddToRoleAsync(user, role);
        }
    }
}

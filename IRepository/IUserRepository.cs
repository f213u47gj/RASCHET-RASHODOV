using RASCHET_HASHODOV.Models;
using RASCHET_HASHODOV.ViewModels.forUser;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RASCHET_HASHODOV.IRepositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(string userId);
        Task<bool> ChangeUserPassword(string userId, string oldPassword, string newPassword);
        Task<bool> LoginUser(string userName, string password, bool rememberMe);
        Task<bool> RegisterUser(RegistrationViewModel model);
        Task Logout();
    }
}

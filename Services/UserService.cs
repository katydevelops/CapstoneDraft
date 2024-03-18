using CapstoneDraft.Data;
using CapstoneDraft.Models;
using Microsoft.AspNetCore.Identity;

namespace CapstoneDraft.Services
{
    public class UserService
    {
        private readonly CapstoneContext _databaseConnection;
        private readonly SignInManager<UserModel> _loginManager;
        private readonly UserManager<UserModel> _userManager;


        public UserService(
            CapstoneContext databaseConnection,
            UserManager<UserModel> userManager,
            SignInManager<UserModel> loginManager)
        {
            _databaseConnection = databaseConnection;
            _loginManager = loginManager;
            _userManager = userManager;
        }

        // Utilize built
        public async Task<UserModel> FetchUserIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }


    }
}

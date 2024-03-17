using CapstoneDraft.Data;
using CapstoneDraft.Models;
using Microsoft.AspNetCore.Identity;

namespace CapstoneDraft.Services
{
    public class UserService
    {
        private readonly CapstoneContext _databaseConnection;
        private readonly SignInManager<UserModel> _loginManager;
        private readonly UserManager<UserModel> userManager;
    }
}

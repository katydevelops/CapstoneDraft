using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CapstoneDraft.Models;

namespace CapstoneDraft.Pages
{
    // LoginModel will inherit from the PageModel class in ASP.NET
    public class LoginModel : PageModel // Example of inheritance in this project
    {
        private readonly SignInManager<UserModel> _loginManager;


        public LoginModel(SignInManager<UserModel> loginManager)
        {
            _loginManager = loginManager;
        }
    }
}

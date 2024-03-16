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

        // Safety Net utilizes ASP.NET Identity for authentication and authorization
        // Constructor to load SignInManager
        public LoginModel(SignInManager<UserModel> loginManager)
        {
            _loginManager = loginManager;
        }

        // BindProperty will capture data from input fields that the user enters during login
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }
    }
}

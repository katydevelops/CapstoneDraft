using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using CapstoneDraft.Models;

namespace CapstoneDraft.Pages
{
    public class LogoutModel : PageModel // Example of inheritance which is required in this assignment
    {
        private readonly SignInManager<UserModel> _signOutManager;

        // Constructor to initialize the SignInManager package of ASP.Net Identity which will be used in this page specifically to log the user out of the application
        public LogoutModel(SignInManager<UserModel> signOutManager)
        {
            _signOutManager = signOutManager;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // The form post from the Logout razor page will trigger the built-in Sign Out Async method which will handling the sign out process and redirect the user to the login page
                await _signOutManager.SignOutAsync();
                return LocalRedirect("/");
        }
    }
}
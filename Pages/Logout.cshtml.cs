using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CapstoneDraft.Pages
{
    [AllowAnonymous] // Allow access without requiring login
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public LogoutModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnPostAsync(string command)
        {
            if (command == "logout")
            {
                await _signInManager.SignOutAsync();
                return RedirectToPage("/Login");
            }

            return RedirectToPage("/");
        }
    }
}
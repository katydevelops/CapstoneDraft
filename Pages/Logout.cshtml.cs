using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using CapstoneDraft.Models;
using Microsoft.Extensions.Logging;

namespace CapstoneDraft.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<UserModel> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<UserModel> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        // This method is called when navigating to /Logout, showing the confirmation message
        public void OnGet()
        {
            // Log when the logout page is accessed
            _logger.LogInformation("Accessed Logout page");
        }

        // This method is called when the user confirms the logout action
        public async Task<IActionResult> OnPostAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage("/Login"); // Adjust this to your login page's actual path
        }
    }
}
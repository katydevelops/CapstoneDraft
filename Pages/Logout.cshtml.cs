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

        public async Task<IActionResult> OnPostAsync()
        {
            _logger.LogInformation("Onpost is being trigger right now: ");
            try
            {
                await _signInManager.SignOutAsync();
                _logger.LogInformation("User logged out successfully.");
                return RedirectToPage("/"); // Adjust this to your login page's actual path
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logging out.");
                // Optionally redirect to an error page or return to the current page with an error message
                return RedirectToPage("/Error");
            }
        }
    }
}
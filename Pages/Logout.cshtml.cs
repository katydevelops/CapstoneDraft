using Microsoft.AspNetCore.Identity;
using CapstoneDraft.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CapstoneDraft.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<UserModel> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly NavigationManager _navigationManager;

        public LogoutModel(
            SignInManager<UserModel> signInManager,
            ILogger<LogoutModel> logger,
            NavigationManager navigationManager)
        {
            _signInManager = signInManager;
            _logger = logger;
            _navigationManager = navigationManager;
        }

        public void OnGet()
        {
            // Log when the logout page is accessed
            _logger.LogInformation("Accessed Logout page");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _logger.LogInformation("Onpost is being triggered right now: ");
            try
            {
                await _signInManager.SignOutAsync();
                _logger.LogInformation("User logged out successfully.");

                // Since we're performing a Blazor component navigation, we don't need to return an IActionResult.
                _navigationManager.NavigateTo("/feed"); // Use NavigationManager to navigate
                return new EmptyResult(); // Return an empty result to complete the HTTP request
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
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
        private readonly ILogger<LogoutModel> _logger; // Add a logger

        // Inject both SignInManager and ILogger into the constructor
        public LogoutModel(SignInManager<UserModel> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            _logger.LogInformation("User logout initiated."); // Log the start of a logout attempt
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out successfully."); // Log successful logout

            // If RedirectToPage("/") does not work as expected, you may need to specify the exact page route
            return RedirectToPage("/feed"); // Assuming there is an Index.cshtml or adjust according to your correct page route
        }
    }
}
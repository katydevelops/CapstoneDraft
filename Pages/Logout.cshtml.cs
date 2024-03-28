using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using CapstoneDraft.Models;

namespace CapstoneDraft.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<UserModel> _signInManager;

        public LogoutModel(SignInManager<UserModel> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage("/");
        }
    }
}
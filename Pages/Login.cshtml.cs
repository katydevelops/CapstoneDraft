using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CapstoneDraft.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<User> _loginManager;

        public void OnGet()
        {
        }
    }
}

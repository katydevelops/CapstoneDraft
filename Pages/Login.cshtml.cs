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

        // Create error message variable that will display relevant messages to user
        public string LoginFormErrorMessage { get; set; }

        // BindProperty will capture data from input fields that the user enters during login
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }

        // Need OnPostAsync handler because the user is sending an HTTP Post request when they click the login button
        // Use IActionResult return type because method will need to handle various login situations such as the user being able to login or a user getting locked out
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                // PasswordSignInAsync provided by ASP Identity will check if the password matches the correct on in the database and update to either true or false
                // IsPersistent will be marked to false for this app to meet the security requirement of assignment but preventing users from staying logged in to the app
                // LockoutOnFailure will be marked to true to meet security requirement of assingment from preventing possible brute force attacks
                var userLogin = await _loginManager.PasswordSignInAsync(Username, Password, isPersistent: false, lockoutOnFailure: true);
                if (userLogin.Succeeded)
                {
                    // Capture user's last active timestamp in order to use in report section
                    var loggedInUser = await _loginManager.UserManager.FindByNameAsync(Username);
                    if (loggedInUser != null)
                    {
                        loggedInUser.UserLastActiveTimeStamp = DateTime.UtcNow;
                        await _loginManager.UserManager.UpdateAsync(loggedInUser);
                    }
                    // If the login is successful, redirect the authenticated user over to the main feed page
                    return LocalRedirect("~/feed");
                }
                // Since lockout failure is turned on, enable a specific error message to let the user know and also to meet security requirement of assignment
                else if (userLogin.IsLockedOut)
                {
                    LoginFormErrorMessage = "Your account has been locked due to too many failed attempts!";
                }
                else
                {
                    LoginFormErrorMessage = "Your login has failed. Please check your username and password and try again";
                }
            }
            catch (Exception error)
            {
                LoginFormErrorMessage = $"Login failed due to the following issue {error.Message}";
            }
            return Page();
        }
    }
}

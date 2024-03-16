using System.ComponentModel.DataAnnotations;

namespace CapstoneDraft.DataAnnotations
{
    public class UsernameAnnotation : ValidationAttribute
    {
        public override bool ValidUsername(object value)
        {
            var validatedUsername = value as string;
            if (string.IsNullOrEmpty(validatedUsername))
            {
                ErrorMessage = "Please enter a username!";
            }
            if (validatedUsername.Length < 7 || validatedUsername.Length > 14)
            {
                ErrorMessage = "Your username must be between 7 and 14 characters long!";
            }
        }
    }
}

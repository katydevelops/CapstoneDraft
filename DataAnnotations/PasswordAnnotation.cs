using System.ComponentModel.DataAnnotations;

namespace CapstoneDraft.DataAnnotations
{
    public class PasswordAnnotation : ValidationAttribute // Example of inheritance for assignment requirements
    {
        // Overriding base IsValid class enables polymorphism of password object per assignment requirements
        public override bool IsValid(object value)
        {
            // Creating custom password annotation including requiring a number and a special character to meet stadard security requirement for assignment
            var validPassword = value as string;
            if (validPassword.Length < 10)
            {
                ErrorMessage = "Your password must be at least 10 characters long!";
                return false;
            }
            if(!validPassword.Any(char.IsDigit))
            {
                ErrorMessage = "Your password must contain at least one number!";
                return false;
            }
            if (!validPassword.Any(specialChar => !char.IsLetterOrDigit(specialChar)))
            {
                ErrorMessage = "Your password must contain at least one alphanumeric character!";
                return false;
            }
            return true;
        }
    }
}

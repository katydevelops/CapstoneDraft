using System.ComponentModel.DataAnnotations;

namespace CapstoneDraft.DataAnnotations
{
    public class PasswordAnnotation : ValidationAttribute // Example of inheritance for assignment requirements
    {
        public override bool IsValid(object value)
        {
            // Creating custom password annotation to meet stadard security requirement for assignment
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
            return true;
        }
    }
}

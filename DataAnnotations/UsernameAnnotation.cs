using System.ComponentModel.DataAnnotations;

namespace CapstoneDraft.DataAnnotations
{
    public class UsernameAnnotation : ValidationAttribute
    {
        // Custom username annotation created to meet standard security requirements for this assignment
        public override bool IsValid(object value)
        {
            var validatedUsername = value as string;
            if (validatedUsername.Length < 7 || validatedUsername.Length > 14)
            {
                ErrorMessage = "Your username must be between 7 and 14 characters long!";
                return false;
            }
            return true;
        }
    }
}

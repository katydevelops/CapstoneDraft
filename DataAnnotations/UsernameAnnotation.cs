using System.ComponentModel.DataAnnotations;

namespace CapstoneDraft.DataAnnotations
{
    public class UsernameAnnotation : ValidationAttribute // Example of inheritance for assignment requirements
    {
        // Overriding the base IsValid class is an example that allows for polymorphism of the username object when it's called throughout the app 
        public override bool IsValid(object value)
        {
            // Custom username annotation created to meet standard security requirements for this assignment
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

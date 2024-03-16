using CapstoneDraft.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace CapstoneDraft.Models
{
    public class RegisterModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required] 
        public string LastName { get; set; }

        // Using customer DataAnnotations for Username and Password format requirements
        [Required]
        [UsernameAnnotation]
        public string Username { get; set; }



        // Using ASP.NET built in DataAnnotations for phone and email format
    }
}

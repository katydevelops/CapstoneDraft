using System.ComponentModel.DataAnnotations;

namespace CapstoneDraft.Models
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)] // Link property to password field in Login razor page 
        public string Password { get; set; }
    }
}

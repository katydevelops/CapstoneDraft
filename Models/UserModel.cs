using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CapstoneDraft.Models
{
    // Inheriting from ASP.NET IdentityUser is a convenient way to take avantage of the built-in functionality from the Identity API such as PasswordHash and LockedOutEnable(to meet the security requiremennts of this assignment) as well as automatically preventing duplicate usernames and emails from being stored in the database
    public class UserModel : IdentityUser // Example of inheritance in Safety Net
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        //public DateTime CreatedTimeStamp { get; set; }
        public DateTime? UserLastActiveTimeStamp { get; set; }

        // Create collection data structure to store the user's affiliated posts and comments
        public virtual ICollection<PostModel> UsersPosts { get; set; } = new List<PostModel>();
        public virtual ICollection<CommentModel> UsersComments { get; set; } = new List<CommentModel>();

        // Store the user creation times in UTC in the database
        public UserModel ()
        {
            //CreatedTimeStamp = DateTime.UtcNow;
            UserLastActiveTimeStamp = DateTime.UtcNow;
        }
    }
}

using Microsoft.AspNetCore.Identity;

namespace CapstoneDraft.Models
{
    public class UserModel : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedTimeStamp { get; set; }
        public DateTime? UserLastActiveTimeStamp { get; set; }

        // Create collection data structure to store the user's affiliated posts and comments
        public virtual ICollection<Post> UsersPosts { get; set; } = new List<Post>();
        public virtual ICollection<Comment> UsersComments { get; set; } = new List<Comment>();

        // Store the user creation times in UTC in the database
        public UserModel ()
        {
            CreatedTimeStamp = DateTime.UtcNow;
            UserLastActiveTimeStamp = DateTime.UtcNow;
        }
    }
}

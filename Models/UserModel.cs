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
        public virtual ICollection<PostModel> UsersPosts { get; set; } = new List<PostModel>();
        public virtual ICollection<CommentModel> UsersComments { get; set; } = new List<CommentModel>();

        // Store the user creation times in UTC in the database
        public UserModel ()
        {
            CreatedTimeStamp = DateTime.UtcNow;
            UserLastActiveTimeStamp = DateTime.UtcNow;
        }
    }
}

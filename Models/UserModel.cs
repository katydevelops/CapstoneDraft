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

    }
}

using System.ComponentModel.DataAnnotations;

namespace CapstoneDraft.Models
{
    public class CommentModel
    {
        [Key]
        public int CommentId { get; set; }

        public string UserId { get; set; } // Links the comment to the correct user who posted it; need to set to string as that's what IdentityUser will use
        public int PostId { get; set; } // Links comment to the affiliated post
        public string CommentText { get; set; }
        public DateTime CommentCreatedTimestamp { get; set; } = DateTime.UtcNow;

        public PostModel Post { get; set; }
        public virtual UserModel User { get; set; }
    }
}

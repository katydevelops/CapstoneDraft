using System.ComponentModel.DataAnnotations;

namespace CapstoneDraft.Models
{
    public class PostModel
    {
        [Key]
        public int PostId { get; set; }
        public string UserId { get; set; } // Links the post to the correct user; need to set to string as that's what IdentityUser will use
        [Required]
        public string PostCategory { get; set; }
        [Required]
        public string AuthorName { get; set; }
        [Required]
        public string UserLocation { get; set; }
        [Required]
        public string PostSubject { get; set; }
        [Required]
        public string PostMessageBody { get; set; }
        public string? PostPhoto { get; set; } // Allow photo to be nullable incase user doesn't want to post a photo
        public DateTime PostCreatedTimestamp { get; set; } = DateTime.UtcNow;
        public int PostLikes { get; set; } = 0; // Initialize post likes to zero
        public virtual UserModel User { get; set; }
        public virtual ICollection<CommentModel> PostComments { get; set; } = new List<CommentModel>();
    }
}

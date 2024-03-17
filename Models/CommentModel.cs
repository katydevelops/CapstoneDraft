namespace CapstoneDraft.Models
{
    public class CommentModel
    {
        public int CommentId { get; set; }
        public int UserId { get; set; } // Links the comment to the correct user who posted it
        public int PostId { get; set; } // Links comment to the affiliated post
        public string CommentText { get; set; }
        public DateTime CommentCreatedTimestamp { get; set; } = DateTime.UtcNow;

        public PostModel Post { get; set; }
        public virtual UserModel User { get; set; }
    }
}

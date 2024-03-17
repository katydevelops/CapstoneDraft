namespace CapstoneDraft.Models
{
    public class CommentModel
    {
        public int CommentId { get; set; }
        public int CommentUserId { get; set; } // Links the comment to the correct user who posted it
        public int PostId { get; set; } // Links comment to the affiliated post
    }
}

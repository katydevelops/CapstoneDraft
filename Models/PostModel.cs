namespace CapstoneDraft.Models
{
    public class PostModel
    {
        public int PostId { get; set; }
        public string PostUserId { get; set; }
        public string PostCategory { get; set; }
        public string NameOfPostAuthor { get; set; }
        public string UserLocation { get; set; }
        public string PostSubject { get; set; }
        public string PostBody { get; set; }
        public string? PostPhoto { get; set; }
        public DateTime PostCreatedTimestamp { get; set; } = DateTime.UtcNow;
        public int PostLikes { get; set; } = 0; // Initialize post likes to zero
        public virtual UserModel PostAuthor { get; set; }
        public virtual ICollection<CommentModel> PostComments { get; set; } = new List<CommentModel>();
    }
}

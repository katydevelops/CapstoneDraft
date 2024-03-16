﻿using System.ComponentModel.DataAnnotations;

namespace CapstoneDraft.Models
{
    public class PostModel
    {
        public int PostId { get; set; }
        public string PostUserId { get; set; }
        [Required]
        public string PostCategory { get; set; }
        [Required]
        public string NameOfPostAuthor { get; set; }
        [Required]
        public string UserLocation { get; set; }
        [Required]
        public string PostSubject { get; set; }
        [Required]
        public string PostMessageBody { get; set; }
        public string? PostPhoto { get; set; } // Allow photo to be nullable incase user doesn't want to post a photo
        public DateTime PostCreatedTimestamp { get; set; } = DateTime.UtcNow;
        public int PostLikes { get; set; } = 0; // Initialize post likes to zero
        public virtual UserModel PostAuthor { get; set; }
        public virtual ICollection<CommentModel> PostComments { get; set; } = new List<CommentModel>();
    }
}
using CapstoneDraft.Data;
using CapstoneDraft.Models;
using Microsoft.EntityFrameworkCore;

namespace CapstoneDraft.Services
{
    public class PostService
    {
        private readonly CapstoneContext _databaseConnection;

        public PostService(CapstoneContext databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        // Fetch the post details and include the author name, comments, user who posted the comments and order by most recent to older posts
        public async Task<List<PostModel>> FetchFeedPostAsync()
        {
            return await _databaseConnection.Posts.Include(post => post.AuthorName).Include(post => post.PostComments).ThenInclude(comment => comment.User).OrderByDescending(post => post.PostCreatedTimestamp).ToListAsync();
        }
    }
}

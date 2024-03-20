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

        // Fetch the post details and include the post details, affiliated comments, user who posted the comments and order by most recent to older posts
        public async Task<List<PostModel>> FetchFeedPostAsync()
        {
            return await _databaseConnection.Posts.Include(post => post.PostComments).ThenInclude(comment => comment.User).OrderByDescending(post => post.PostCreatedTimestamp).ToListAsync();
        }

        // Add the newly created post to the database using built-in Entity Framework method
        public async Task AddNewPostAsync(PostModel postModel)
        {
            _databaseConnection.Posts.Add(postModel);
            await _databaseConnection.SaveChangesAsync();
        }

        public async Task <List<PostModel>> QueryPostsAndCommentsAsync(string searchQuery)
        {

        }
    }
}

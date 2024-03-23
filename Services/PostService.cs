using CapstoneDraft.Data;
using CapstoneDraft.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

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
        public async Task AddNewPostAsync(PostModel post)
        {
            _databaseConnection.Posts.Add(post);
            await _databaseConnection.SaveChangesAsync();
        }

        public async Task <List<PostModel>> QueryPostsAndCommentsAsync(string searchQuery)
        {
            try
            {
                var lowerCaseQuery = searchQuery.ToLower() ?? string.Empty;
                var queryResults = await _databaseConnection.Posts.Include(post => post.PostComments).Where(post => EF.Functions.Like(post.PostSubject.ToLower(), $"%{lowerCaseQuery}%") ||
                    EF.Functions.Like(post.PostSubject.ToLower(), $"%{lowerCaseQuery}%") ||
                    EF.Functions.Like(post.PostMessageBody.ToLower(), $"%{lowerCaseQuery}%") ||
                    EF.Functions.Like(post.AuthorName.ToLower(), $"%{lowerCaseQuery}%") ||
                    EF.Functions.Like(post.PostCategory.ToLower(), $"%{lowerCaseQuery}%") ||
                    EF.Functions.Like(post.UserLocation.ToLower(), $"%{lowerCaseQuery}%") ||
                    post.PostComments.Any(comments => EF.Functions.Like(comments.CommentText.ToLower(), $"%{lowerCaseQuery}%"))).ToListAsync();
                return queryResults;
            }
            catch
            {
                throw;
            }
        }

        public async Task<PostModel> FetchPostAsync(int postId)
        {
            return await _databaseConnection.Posts.Include(post => post.User).Include(post => post.PostComments).FirstOrDefaultAsync(post => post.PostId == postId);
        }

        public async Task EditPostAsync(PostModel post)
        {
            // Observe the changes that were made to the post using the EF Modified keyword - this will submit the updated values to the database when SaveChangesAsync is called
            _databaseConnection.Entry(post).State = EntityState.Modified;
            await _databaseConnection.SaveChangesAsync(); 
        }

        public async Task RemovePostAsync(int postId)
        {
            var postPendingDeletion = await _databaseConnection.Posts.FindAsync(postId);
            if (postPendingDeletion != null)
            {
                _databaseConnection.Posts.Remove(postPendingDeletion);
                await _databaseConnection.SaveChangesAsync();
            }
        }

        public async Task UpdateCommentAsync(int commentId, string newBody, string userId)
        {
            var commentPendingUpdate = await _databaseConnection.Comments.FirstOrDefault(comment => comment.CommentId == commentId);
            if (commentPendingUpdate != null && commentPendingUpdate.UserId == userId) 
            { 
                commentPendingUpdate.Body = newBody;
                _databaseConnection.Comments.Update(commentPendingUpdate)
                await _databaseConnection.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("This comment could not be located - please try again!");
            }
        }
    }
}

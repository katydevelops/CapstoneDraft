using CapstoneDraft.Data;
using CapstoneDraft.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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

        public async Task<List<PostModel>> QueryPostsAndCommentsAsync(string searchQuery)
        {
            // If the search term is empty then return an empty list in return
            if (string.IsNullOrWhiteSpace(searchQuery)) 
            {
                return new List<PostModel>();
            }
            try
            {
                // Convert the search query to lowercase to prevent case sensitivity and then use Entity Framework Core like operation to query the posts and comments where the search query matches the related post and comment properties stored in the database
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
            catch (Exception error)
            {
                throw new ArgumentException("There was an error processing your search - please try again!", error);
            }
        }

        public async Task<PostModel> FetchPostAsync(int postId)
        {
            // Fetch the post from the database that matches the post id and return affiliated post comments as well
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
            // Find the post that matches the id in the database, store it to the pending deletion variable and performa a null check. If the post isn't null then delete it from the database and save accordingly
            var postPendingDeletion = await _databaseConnection.Posts.FindAsync(postId);
            if (postPendingDeletion != null)
            {
                _databaseConnection.Posts.Remove(postPendingDeletion);
                await _databaseConnection.SaveChangesAsync();
            }
        }

        public async Task UpdateCommentAsync(int commentId, string newCommentText, string userId)
        {
            // Find the comment that matches the comment id and make sure the comment isn't null and the user is the author of the comment based on their user id. After the user is authorized to make changes up the comment to the new comment text entered and then save the changes inthe database.
            var commentPendingUpdate = await _databaseConnection.Comments.Where(comment => comment.CommentId == commentId).FirstOrDefaultAsync();
            if (commentPendingUpdate != null && commentPendingUpdate.UserId == userId)
            {
                commentPendingUpdate.CommentText = newCommentText;
                _databaseConnection.Comments.Update(commentPendingUpdate);
                await _databaseConnection.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("This comment could not be located - please try again!");
            }
        }

        public async Task<bool> RemoveCommentAsync(int commentId, string userId)
        {
            // Find the comment that matches the comment id and make sure the comment isn't null and the user is the author of the comment based on their user id. After the user is authorized, remove the comment per their request from the database and save accordingly.
            var commentPendingDeletion = await _databaseConnection.Comments.FirstOrDefaultAsync(comment => comment.CommentId == commentId);
            if (commentPendingDeletion != null && commentPendingDeletion.UserId == userId)
            {
                _databaseConnection.Comments.Remove(commentPendingDeletion);
                await _databaseConnection.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task AddCommentAsync(CommentModel comment)
        {
            if (comment == null || string.IsNullOrWhiteSpace(comment.CommentText))
            {
                return;
            }
            try
            {
                _databaseConnection.Comments.Add(comment);
                await _databaseConnection.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<PostModel>> FetchPostsByCategoryAsync(string category)
        {
            // Grab all the posts that match the category entered as a parameter and return them as a list
            return await _databaseConnection.Posts.Where(post => post.PostCategory == category).ToListAsync();
        }
    }
}

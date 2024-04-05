using CapstoneDraft.Data;
using Microsoft.EntityFrameworkCore;

namespace CapstoneDraft.Services
{
    public class ReportService
    {
        private readonly CapstoneContext _databaseConnection;

        public ReportService(CapstoneContext databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public async Task<Dictionary<string, (int TotalPosts, DateTime? LatestPostTimestamp)>> GetTotalPostsPerUserAsync()
        {
            // Receive the total number of posts from the database based on the user object and returns the information as a dictionary where the total post number and latest post timestamp are returned as the values to the user key.
            var totalPostPerUser = await _databaseConnection.Users.Select(user => new
            {
                Username = user.UserName,
                TotalPosts = user.UsersPosts.Count(),
                LatestPostTimestamp = user.UsersPosts.Max(post => (DateTime?)post.PostCreatedTimestamp)
            }).ToDictionaryAsync(user => user.Username, user => (user.TotalPosts, user.LatestPostTimestamp));
            return totalPostPerUser;
        }

        public async Task<Dictionary<string, (int TotalComments, DateTime? LatestCommentTimestamp)>> GetTotalCommentsPerUserAsync()
        {
            // Receive the total number of comments from the database based on the user object and returns the information as a dictionary where the total comment number and latest comment timestamp are returned as the values to the user key.
            return await _databaseConnection.Comments.GroupBy(comment => comment.User.UserName)
                .Select(group => new
                {
                    UserName = group.Key,
                    TotalComments = group.Count(),
                    LatestCommentTimestamp = group.Max(comment => (DateTime?)comment.CommentCreatedTimestamp)
                }).ToDictionaryAsync(group => group.UserName, group => (group.TotalComments, group.LatestCommentTimestamp));
        }

        public async Task<Dictionary<string, int>> GetTotalPostsPerCategoryAsync()
        {
            // Return the total number count of each type of post category and return as a dictionary where the post category is the key and the count is the value
            return await _databaseConnection.Posts.GroupBy(post => post.PostCategory).Select(group => new { PostCategory = group.Key, Count = group.Count() }).ToDictionaryAsync(group => group.PostCategory, group => group.Count);
        }

        public async Task<List<(string UserName, string Email, DateTime? UserLastActive)>> GetUserLastActiveDetailsAsync()
        {
            // Get the user's username, email and last active timestamp and return these details as a list storted by the user's with the most recent login timestamp to the user's who have not logged in recently.
            var userLastActive = await _databaseConnection.Users
                .Select(user => new
                {
                    user.UserName,
                    user.Email,
                    UserLastActive = user.UserLastActiveTimeStamp,
                })
                .OrderByDescending(user => user.UserLastActive).ToListAsync();
            return userLastActive.Select(user => (user.UserName, user.Email, user.UserLastActive)).ToList();
        }
    }


}

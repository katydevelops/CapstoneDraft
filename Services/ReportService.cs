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
            return await _databaseConnection.Posts.GroupBy(post => post.PostCategory).Select(group => new { PostCategory = group.Key, Count = group.Count() }).ToDictionaryAsync(group => group.PostCategory, group => group.Count);
        }

        public async Task<List<(string UserName, string Email, DateTime? UserLastActive)>> GetUserLastActiveDetailsAsync()
        {

        }
    }


}

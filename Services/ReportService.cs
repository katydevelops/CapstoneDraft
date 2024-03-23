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

        public async Task<Dictionary<string, (int TotalComments, DateTime? LatestCommentTimestamp)>> GetTotalCommentsPerUser()
        {

        }
    }


}

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

        public async Task<Dictionary<string, (int PostCount, DateTime? LatestPostTimestamp)>> GetTotalPostsPerUserAsync()
        {
            var totalPostPerUser = await _databaseConnection.Users.Select(user => new
            {
                Username = user.UserName,
                TotalPost = user.UsersPosts.Count(),
                LatestPostTimestamp = user.UsersPosts.Max(post => (DateTime?)post.PostCreatedTimestamp)
            }).ToDictionaryAsync(user => user.Username, user => (user.TotalPost, user.LatestPostTimestamp));
            return totalPostPerUser;
        }
    }


}

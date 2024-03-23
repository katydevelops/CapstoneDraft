using CapstoneDraft.Data;

namespace CapstoneDraft.Services
{
    public class ReportService
    {
        private readonly CapstoneContext _databaseConnection;

        public ReportService(CapstoneContext databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public async Task<Dictionary<string, (int PostCount, DateTime? LatestPostDate)>> GetTotalPostsPerUserAsync()
        {
            var totalPostPerUser = await _databaseConnection.Users.Select(user => new
            {
                Username = user.UserName,
                TotalPost = user.UsersPosts.Count(),

            });
        }
    }


}

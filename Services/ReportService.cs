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

        }
    }


}

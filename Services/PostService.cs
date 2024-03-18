using CapstoneDraft.Data;

namespace CapstoneDraft.Services
{
    public class PostService
    {
        private readonly CapstoneContext _databaseConnection;

        public PostService(CapstoneContext databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }
    }
}

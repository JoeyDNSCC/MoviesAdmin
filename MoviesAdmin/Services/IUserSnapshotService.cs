namespace MoviesAdmin.Services
{
    public interface IUserSnapshotService
    {
        public Task AddUserAsync(string userId, string email, string name);
    }
}



using ChatApplication.Models;

namespace ChatApplication.Repository
{
    public interface IAppUserRepository
    {
        Task<AppUser> GetUserByNameAsync(string username);
    }
}

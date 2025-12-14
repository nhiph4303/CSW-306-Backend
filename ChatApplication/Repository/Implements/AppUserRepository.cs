using ChatApplication.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace ChatApplication.Repository.Implements
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly UserManager<AppUser> _userManager;

        public AppUserRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AppUser> GetUserByNameAsync(string username)
        {
            var receiverUser = await _userManager.FindByNameAsync(username);
            return receiverUser;
        }
    }
}

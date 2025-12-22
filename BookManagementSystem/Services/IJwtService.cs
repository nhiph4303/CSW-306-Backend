using BookManagementSystem.Models;

namespace BookManagementSystem.Services
{
    public interface IJwtService
    {
        public string GenerateJwtToken(User user);
    }
}

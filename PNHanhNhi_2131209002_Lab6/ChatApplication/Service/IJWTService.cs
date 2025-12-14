using ChatApplication.Models;

namespace ChatApplication.Service
{
    public interface IJWTService
    {
        string GenerateJwtToken(AppUser user);
    }
}

using LeThiMinhTuoi_Lab6_Backend.Models;

namespace LeThiMinhTuoi_Lab6_Backend.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(AppUser user);

    }
}

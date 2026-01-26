using Project.Core.Entities.Models;

namespace Project.Core.Utilities.Security.Jwt
{
    public interface IJwtService
    {
        AccessToken CreateToken(User user);
        bool ValidateRefreshToken(string refreshToken);

    }
}

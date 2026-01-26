using Project.Core.Utilities.Results;
using Project.Entities.Dtos.System.AuthDtos;

namespace Project.DataAccess.Repositories.Abstract.System
{
    public interface IAuthRepository
    {
        Result Login(UserLoginDto model);
        Result LogOut();
        Result RefreshTokenLogin(string refreshToken);
    }
}

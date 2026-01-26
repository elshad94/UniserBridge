using AutoMapper;
using Project.Core.Entities.Models;
using Project.Core.Settings;
using Project.Core.Utilities.Results;
using Project.Core.Utilities.Security;
using Project.Core.Utilities.Security.Jwt;
using Project.Entities.Dtos.System.AuthDtos;
using Project.DataAccess.Repositories.Abstract.System;

namespace Project.DataAccess.Repositories.Concrete.System
{
    public class AuthRepository : IAuthRepository
    {


        private readonly IUserLoginHistoryRepository _userLoginHistoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserTokenRepository _userTokenRepository;
        private readonly IJwtService _jwtManager;
        private readonly IMapper _mapper;


        private JwtOptions _jwtOptions;

        public AuthRepository(IJwtService jwtManager, IMapper mapper, IUserRepository userRepository, IUserTokenRepository UserTokenRepository, IUserLoginHistoryRepository userLoginHistoryRepository)
        {

            _jwtManager = jwtManager;
            _mapper = mapper;
            _userRepository = userRepository;
            _userTokenRepository = UserTokenRepository;
            _jwtOptions = AppSettings.Settings.JwtOptions;
            _userLoginHistoryRepository = userLoginHistoryRepository;
        }



        public Result LogOut()
        {
            var result = new Result();

            var userTokenData = _userTokenRepository.Get(x =>
                  x.UserId == CurrentScopeDataContainer.Instance.UserId);

            if (userTokenData != null)
            {
                userTokenData.LogOut = true;
                _userTokenRepository.Update(userTokenData);
                result.ResultInfo = ResultInfo.TokenExpired;

            }
            else
            {
                result.ResultInfo = ResultInfo.UnexpectedError;

            }
            return result;

        }



        public Result Login(UserLoginDto model)
        {
            var result = new Result();

            var userData = _userRepository.Get(x => x.Username == model.Username &&
            x.Password == SecurityHelper.CreateMD5(model.Password) && x.Status == true);

            if (userData == null)
            {
                result.ResultInfo = ResultInfo.UserNamePasswordIncorrect;
                return result;
            }


            var createToken = GenerateAccessToken(userData);

            if (userData != null && createToken.Token != null)
            {
                var userTokenData = _userTokenRepository.Get(x =>
                x.UserId == userData.Id);

                //Userin-in tokeni varsa token melumatlarini yenile 
                if (userTokenData != null)
                {
                    userTokenData.AccessToken = createToken.Token;
                    userTokenData.RefreshToken = createToken.RefreshToken;
                    userTokenData.UserId = userData.Id;
                    userTokenData.EndDate = createToken.Expiration.AddMinutes(_jwtOptions.AccessTokenExpiration);
                    userTokenData.LogOut = false;
                    _userTokenRepository.Update(userTokenData);

                }
                else if (userTokenData == null) //yoxdursa elave et

                {
                    UserToken userToken = new();
                    userToken.AccessToken = createToken.Token;
                    userToken.RefreshToken = createToken.RefreshToken;
                    userToken.UserId = userData.Id;
                    userToken.EndDate = createToken.Expiration.AddMinutes(_jwtOptions.AccessTokenExpiration);
                    userToken.LogOut = false;
                    _userTokenRepository.Add(userToken);
                }

            }



            _userLoginHistoryRepository.Add(new UserLoginHistory { LoginDate = DateTime.Now, UserId = userData.Id });

            result.Data = createToken;
            return result;


        }

        public Result RefreshTokenLogin(string refreshToken)
        {
            var result = new Result();

            if (_jwtManager.ValidateRefreshToken(refreshToken))
            {
                var refreshTokenData = _userTokenRepository.Get(x => x.RefreshToken == refreshToken &&
                !x.LogOut);

                if (refreshTokenData != null)
                {
                    var userData = _userRepository.Get(x => x.Id == refreshTokenData.UserId);

                    var token = GenerateAccessToken(userData);

                    if (userData != null && token != null)
                    {
                        refreshTokenData.AccessToken = token.Token;
                        refreshTokenData.RefreshToken = token.RefreshToken;
                        refreshTokenData.UserId = userData.Id;
                        refreshTokenData.EndDate = token.Expiration;
                        _userTokenRepository.Update(refreshTokenData);
                    }

                    result.Data = token;

                }
                else
                {
                    result.ResultInfo = ResultInfo.TokenExpired;

                }
            }
            else
            {
                result.ResultInfo = ResultInfo.TokenIsInvalid;

            }


            return result;

        }

        private AccessToken GenerateAccessToken(User user)
        {
            var accessToken = _jwtManager.CreateToken(user);
            return accessToken;
        }








    }
}

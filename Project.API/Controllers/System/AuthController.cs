using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Entities.Dtos.System.AuthDtos;
using Project.DataAccess.Repositories.Abstract.System;

namespace Project.API.Controllers.System
{
    [Route("System/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }


        //[ModelStateControl]
        //[AllowAnonymous]
        //[HttpPost("login")]
        //public IActionResult Login(UserLoginDto userLoginDto)
        //{
        //    // string formattedNumber = CommonTools.GetFormattedDocumentNo("TL24-0002", NumberPrefix.TL);

        //    var data = _authRepository.Login(userLoginDto);

        //    return data.AsObjectResult();
        //}


        //[HttpGet("logout")]
        //public IActionResult LogOut()
        //{

        //    var userLogin = _authRepository.LogOut();

        //    return Ok(new { userLogOutStatus = true });
        //}



        //[AllowAnonymous]
        //[HttpPost("[action]")]
        //public IActionResult RefreshTokenLogin(RefreshTokenRequest refreshToken)
        //{
        //    var data = _authRepository.RefreshTokenLogin(refreshToken.RefreshToken);

        //    return data.AsObjectResult();
        //}




        //[HttpGet("[action]")]
        //public IActionResult CheckAuthStatus()
        //{
        //    return Ok(new { authStatus = true });
        //}


        //[HttpPost("register")]
        //public IActionResult Register(UserRegisterDto userRegisterDto)
        //{
        //    var userExists = _authRepository.UserExists(userRegisterDto.Username);
        //    if (!userExists.Status)
        //    {
        //        return BadRequest(userExists.Message);
        //    }

        //    var registerResult = _authRepository.Register(userRegisterDto);


        //    var GenerateAccessToken = _authRepository.GenerateAccessToken(userLogin.Data);
        //    if (GenerateAccessToken.Status)
        //    {
        //        return Ok(GenerateAccessToken.Data);
        //    }

        //    return BadRequest(GenerateAccessToken.Message);
        //}





    }
}

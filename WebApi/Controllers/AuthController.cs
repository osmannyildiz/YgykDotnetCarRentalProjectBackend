using Business.Abstract;
using Core.Entities.Dtos;
using Core.Extensions;
using Core.Utilities.Ioc;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        private IAuthService _authService;
        private IHttpContextAccessor _httpContextAccessor;

        public AuthController(IAuthService authService) {
            _authService = authService;
            _httpContextAccessor = ServiceHelper.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        [HttpPost("login")]
        public IActionResult Login(UserLoginDto userLoginDto) {
            var loginResult = _authService.Login(userLoginDto);
            if (!loginResult.Success) {
                return BadRequest(new ErrorResult(loginResult.Message));
            }

            var tokenResult = _authService.CreateAccessToken(loginResult.Data);
            if (!tokenResult.Success) {
                return BadRequest(new ErrorResult(tokenResult.Message));
            }

            return Ok(new SuccessDataResult<AccessToken>(tokenResult.Data, loginResult.Message));
        }

        [HttpPost("register")]
        public IActionResult Register(UserRegisterDto userRegisterDto) {
            var userExistsResult = _authService.UserWithEmailAlreadyExists(userRegisterDto.Email);
            if (!userExistsResult.Success) {
                return BadRequest(new ErrorResult(userExistsResult.Message));
            }

            var registerResult = _authService.Register(userRegisterDto);
            if (!registerResult.Success) {
                return BadRequest(new ErrorResult(registerResult.Message));
            }

            var tokenResult = _authService.CreateAccessToken(registerResult.Data);
            if (!tokenResult.Success) {
                return BadRequest(new ErrorResult(tokenResult.Message));
            }

            return Ok(new SuccessDataResult<AccessToken>(tokenResult.Data, registerResult.Message));
        }

        [HttpPost("changePassword")]
        public IActionResult ChangePassword(ChangePasswordFormDataDto formDataDto) {
            int userId = Int32.Parse(_httpContextAccessor.HttpContext.User.GetClaims(ClaimTypes.NameIdentifier)[0]);

            var result = _authService.ChangePassword(userId, formDataDto.CurrentPassword, formDataDto.NewPassword);
            if (!result.Success) {
                return BadRequest(new ErrorResult(result.Message));
            }

            return Ok(new SuccessResult(result.Message));
        }
    }
}

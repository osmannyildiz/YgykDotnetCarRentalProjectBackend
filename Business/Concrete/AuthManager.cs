using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Core.Utilities.Business;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete {
    public class AuthManager : IAuthService {
        private IUserService _userManager;
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userManager, ITokenHelper tokenHelper) {
            _userManager = userManager;
            _tokenHelper = tokenHelper;
        }

        // TODO Password constraints (min length, etc.)
        [ValidationAspect(typeof(UserRegisterDtoValidator))]
        public IDataResult<User> Register(UserRegisterDto userRegisterDto) {
            var errorResult = BusinessEngine.Run(
                CheckIfPasswordDoesNotContainVarietyOfCharacters(userRegisterDto.Password)
            );
            if (errorResult != null) {
                return new ErrorDataResult<User>(errorResult.Message);
            }

            byte[] passwordHash, passwordSalt;
            HashingTool.HashPassword(userRegisterDto.Password, out passwordHash, out passwordSalt);
            var user = new User {
                FirstName = userRegisterDto.FirstName,
                LastName = userRegisterDto.LastName,
                Email = userRegisterDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _userManager.Add(user);
            return new SuccessDataResult<User>(user, Messages.RegisterSuccessful);
        }

        //[ValidationAspect(typeof(UserLoginDtoValidator))]
        public IDataResult<User> Login(UserLoginDto userLoginDto) {
            var userToCheck = _userManager.GetByEmail(userLoginDto.Email).Data;
            if (userToCheck == null) {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingTool.VerifyPasswordHash(userLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt)) {
                return new ErrorDataResult<User>(Messages.WrongPassword);
            }

            return new SuccessDataResult<User>(userToCheck, Messages.LoginSuccessful);
        }

        public IResult UserWithEmailAlreadyExists(string email) {
            if (_userManager.GetByEmail(email).Data != null) {
                return new ErrorResult(Messages.UserWithEmailAlreadyExists);
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user) {
            var claims = _userManager.GetOperationClaims(user).Data;
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }
        private IResult CheckIfPasswordDoesNotContainVarietyOfCharacters(string password) {
            if (!(password.Any(char.IsLower) && password.Any(char.IsUpper) && password.Any(char.IsDigit))) {
                return new ErrorResult(Messages.PasswordDoesNotContainVarietyOfCharacters);
            }
            return new SuccessResult();
        }
    }
}

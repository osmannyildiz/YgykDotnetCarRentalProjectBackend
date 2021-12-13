using Business.Abstract;
using Business.Aspects.Autofac.Auth;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Core.Utilities.Business;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete {
    public class AuthManager : IAuthService {
        private IUserService _userService;
        private ICustomerService _customerService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ICustomerService customerService, ITokenHelper tokenHelper) {
            _userService = userService;
            _customerService = customerService;
            _tokenHelper = tokenHelper;
        }

        // TODO Password constraints (min length, etc.)
        [ValidationAspect(typeof(UserRegisterDtoValidator))]
        public IDataResult<User> Register(UserRegisterDto userRegisterDto) {
            var errorResult = BusinessEngine.Run(
                CheckIfPasswordDoesNotContainVarietyOfCharacters(userRegisterDto.Password),
                CheckIfCustomerWithCompanyNameAlreadyExists(userRegisterDto.CompanyName)
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
            _userService.Add(user);

            var customer = new Customer {
                UserId = user.Id,
                CompanyName = userRegisterDto.CompanyName
            };
            _customerService.Add(customer);

            _userService.AddOperationClaim(user, "user");

            return new SuccessDataResult<User>(user, Messages.RegisterSuccessful);
        }

        //[ValidationAspect(typeof(UserLoginDtoValidator))]
        public IDataResult<User> Login(UserLoginDto userLoginDto) {
            var userToCheck = _userService.GetByEmail(userLoginDto.Email).Data;
            if (userToCheck == null) {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingTool.VerifyPasswordHash(userLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt)) {
                return new ErrorDataResult<User>(Messages.WrongPassword);
            }

            return new SuccessDataResult<User>(userToCheck, Messages.LoginSuccessful);
        }

        [SecuredOperation("user,admin")]
        public IResult ChangePassword(int userId, string currentPassword, string newPassword) {
            var errorResult = BusinessEngine.Run(
                CheckIfPasswordDoesNotContainVarietyOfCharacters(newPassword)
            );
            if (errorResult != null) {
                return new ErrorResult(errorResult.Message);
            }

            var user = _userService.GetById(userId).Data;
            //if (user == null) {
            //    return new ErrorResult(Messages.UserNotFound);
            //}

            if (!HashingTool.VerifyPasswordHash(currentPassword, user.PasswordHash, user.PasswordSalt)) {
                return new ErrorResult(Messages.WrongPassword);
            }

            byte[] passwordHash, passwordSalt;
            HashingTool.HashPassword(newPassword, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _userService.Update(user);

            return new SuccessResult(Messages.PasswordChanged);
        }

        public IResult UserWithEmailAlreadyExists(string email) {
            if (_userService.GetByEmail(email).Data != null) {
                return new ErrorResult(Messages.UserWithEmailAlreadyExists);
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user) {
            var claims = _userService.GetOperationClaims(user).Data;
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        private IResult CheckIfPasswordDoesNotContainVarietyOfCharacters(string password) {
            if (!(password.Any(char.IsLower) && password.Any(char.IsUpper) && password.Any(char.IsDigit))) {
                return new ErrorResult(Messages.PasswordDoesNotContainVarietyOfCharacters);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCustomerWithCompanyNameAlreadyExists(string companyName) {
            if (_customerService.GetByCompanyName(companyName).Data != null) {
                return new ErrorResult(Messages.CustomerWithCompanyNameAlreadyExists);
            }
            return new SuccessResult();
        }
    }
}

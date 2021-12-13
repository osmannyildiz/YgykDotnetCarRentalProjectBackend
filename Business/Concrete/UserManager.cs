using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Aspects.Autofac.Auth;
using Core.Aspects.Autofac.Caching;
using Entities.Dtos;
using Entities.Concrete;

namespace Business.Concrete {
    public class UserManager : IUserService {
        IUserDal _userDal;
        ICustomerService _customerService;

        public UserManager(IUserDal userDal, ICustomerService customerService) {
            _userDal = userDal;
            _customerService = customerService;
        }

        [ValidationAspect(typeof(UserValidator))]
        [CacheRemoveAspect("IUserService.Get")]
        public IResult Add(User user) {
            _userDal.Add(user);
            return new SuccessResult(Messages.UserAdded);
        }

        [ValidationAspect(typeof(UserValidator))]
        [CacheRemoveAspect("IUserService.GetOperationClaim")]
        public IResult AddOperationClaim(User user, string operationClaimName) {
            _userDal.AddOperationClaim(user, operationClaimName);
            return new SuccessResult();
        }

        [ValidationAspect(typeof(UserValidator))]
        [CacheRemoveAspect("IUserService.Get")]
        public IResult Delete(User user) {
            _userDal.Delete(user);
            return new SuccessResult(Messages.UserDeleted);
        }

        [SecuredOperation("admin")]
        [CacheAspect]
        public IDataResult<List<User>> GetAll() {
            return new SuccessDataResult<List<User>>(_userDal.GetAll());
        }

        [CacheAspect]
        public IDataResult<User> GetByEmail(string email) {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Email == email));
        }

        [CacheAspect]
        public IDataResult<User> GetById(int id) {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Id == id));
        }

        [CacheAspect]
        public IDataResult<List<OperationClaim>> GetOperationClaims(User user) {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetOperationClaims(user));
        }

        [CacheAspect]
        public IDataResult<UserInfoDto> GetUserInfoByEmail(string email) {
            return new SuccessDataResult<UserInfoDto>(_userDal.GetUserInfo(u => u.Email == email));
        }

        [CacheAspect]
        public IDataResult<UserInfoDto> GetUserInfoByUserId(int userId) {
            return new SuccessDataResult<UserInfoDto>(_userDal.GetUserInfo(u => u.Id == userId));
        }

        [ValidationAspect(typeof(UserValidator))]
        [CacheRemoveAspect("IUserService.Get")]
        public IResult Update(User user) {
            _userDal.Update(user);
            return new SuccessResult(Messages.UserUpdated);
        }

        [ValidationAspect(typeof(UserValidator))]
        [CacheRemoveAspect("IUserService.Get", "ICustomerService.Get")]
        public IResult UpdateUserInfo(UserInfoDto userInfoDto) {
            var user = _userDal.Get(u => u.Id == userInfoDto.Id);
            user.FirstName = userInfoDto.FirstName;
            user.LastName = userInfoDto.LastName;
            user.Email = userInfoDto.Email;

            var customer = _customerService.GetById(userInfoDto.CustomerId).Data;
            customer.CompanyName = userInfoDto.CustomerCompanyName;

            _userDal.Update(user);
            _customerService.Update(customer);
            return new SuccessResult(Messages.UserInfoUpdated);
        }
    }
}

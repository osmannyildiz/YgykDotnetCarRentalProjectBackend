﻿using Business.Abstract;
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

namespace Business.Concrete {
    public class UserManager : IUserService {
        IUserDal _userDal;

        public UserManager(IUserDal userDal) {
            _userDal = userDal;
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult Add(User user) {
            _userDal.Add(user);
            return new SuccessResult(Messages.UserAdded);
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult Delete(User user) {
            _userDal.Delete(user);
            return new SuccessResult(Messages.UserDeleted);
        }

        [SecuredOperation("admin")]
        public IDataResult<List<User>> GetAll() {
            return new SuccessDataResult<List<User>>(_userDal.GetAll());
        }

        public IDataResult<User> GetByEmail(string email) {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Email == email));
        }

        public IDataResult<User> GetById(int id) {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Id == id));
        }

        public IDataResult<List<OperationClaim>> GetOperationClaims(User user) {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetOperationClaims(user));
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult Update(User user) {
            _userDal.Update(user);
            return new SuccessResult(Messages.UserUpdated);
        }
    }
}

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
using System.Linq.Expressions;
using System.Text;

namespace Business.Concrete {
    public class UserManager : IUserService {
        IUserDal _userDal;

        public UserManager(IUserDal userDal) {
            _userDal = userDal;
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult Add(User user) {
            var errorResult = BusinessEngine.Run(
                CheckIfPasswordDoesNotContainVarietyOfCharacters(user.Password)
            );
            if (errorResult != null) {
                return errorResult;
            }

            _userDal.Add(user);
            return new SuccessResult(Messages.UserAdded);
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult Delete(User user) {
            _userDal.Delete(user);
            return new SuccessResult(Messages.UserDeleted);
        }

        public IDataResult<List<User>> GetAll() {
            return new SuccessDataResult<List<User>>(_userDal.GetAll());
        }

        public IDataResult<User> GetById(int id) {
            return new SuccessDataResult<User>(_userDal.Get(u => u.Id == id));
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult Update(User user) {
            _userDal.Update(user);
            return new SuccessResult(Messages.UserUpdated);
        }

        private IResult CheckIfPasswordDoesNotContainVarietyOfCharacters(string password) {
            if (!(password.Any(char.IsLower) && password.Any(char.IsUpper) && password.Any(char.IsDigit))) {
                return new ErrorResult(Messages.PasswordDoesNotContainVarietyOfCharacters);
            }
            return new SuccessResult();
        }
    }
}

using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
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
        public IResult Add(User entity) {
            // Şifrenin küçük harf, büyük harf ve rakamlardan en az birer adet içermediği durum
            if (!(entity.Password.Any(char.IsLower) && entity.Password.Any(char.IsUpper) && entity.Password.Any(char.IsDigit))) {
                return new ErrorResult(Messages.UserPasswordMustContainTheseTypesOfCharacters);
            }

            _userDal.Add(entity);
            return new SuccessResult(Messages.UserAdded);
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult Delete(User entity) {
            _userDal.Delete(entity);
            return new SuccessResult(Messages.UserDeleted);
        }

        public IDataResult<User> Get(Expression<Func<User, bool>> filter) {
            return new SuccessDataResult<User>(_userDal.Get(filter));
        }

        public IDataResult<List<User>> GetAll(Expression<Func<User, bool>> filter = null) {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(filter));
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult Update(User entity) {
            _userDal.Update(entity);
            return new SuccessResult(Messages.UserUpdated);
        }
    }
}

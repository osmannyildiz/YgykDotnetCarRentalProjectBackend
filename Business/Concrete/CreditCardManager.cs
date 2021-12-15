using Business.Abstract;
using Business.Aspects.Autofac.Auth;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete {
    public class CreditCardManager : ICreditCardService {
        private ICreditCardDal _creditCardDal;

        public CreditCardManager(ICreditCardDal creditCardDal) {
            _creditCardDal = creditCardDal;
        }

        [SecuredOperation("user,admin")]
        //[ValidationAspect(typeof(CreditCardValidator))]
        //[CacheRemoveAspect("ICreditCardService.Get")]
        public IResult Add(CreditCard creditCard) {
            _creditCardDal.Add(creditCard);
            return new SuccessResult(Messages.CreditCardAdded);
        }

        [SecuredOperation("user,admin")]
        //[ValidationAspect(typeof(CreditCardValidator))]
        //[CacheRemoveAspect("ICreditCardService.Get")]
        public IResult Delete(CreditCard creditCard) {
            _creditCardDal.Delete(creditCard);
            return new SuccessResult(Messages.CreditCardDeleted);
        }

        [SecuredOperation("admin")]
        //[CacheAspect]
        public IDataResult<List<CreditCard>> GetAll() {
            return new SuccessDataResult<List<CreditCard>>(_creditCardDal.GetAll());
        }

        [SecuredOperation("user,admin")]
        //[CacheAspect]
        public IDataResult<CreditCard> GetById(int id) {
            return new SuccessDataResult<CreditCard>(_creditCardDal.Get(cc => cc.Id == id));
        }

        [SecuredOperation("user,admin")]
        //[CacheAspect]
        public IDataResult<CreditCard> GetByUserId(int userId) {
            return new SuccessDataResult<CreditCard>(_creditCardDal.Get(cc => cc.UserId == userId));
        }

        [SecuredOperation("user,admin")]
        //[ValidationAspect(typeof(CreditCardValidator))]
        //[CacheRemoveAspect("ICreditCardService.Get")]
        public IResult Update(CreditCard creditCard) {
            _creditCardDal.Update(creditCard);
            return new SuccessResult(Messages.CreditCardUpdated);
        }
    }
}

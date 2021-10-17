using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Concrete {
    public class RentalManager : IRentalService {
        IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal) {
            _rentalDal = rentalDal;
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental entity) {
            var errorResult = BusinessEngine.Run(
                CheckIfRentedCarNotReturnedYet(entity),
                CheckIfRentalReturnDateIsBeforeRentDate(entity)
            );
            if (errorResult != null) {
                return errorResult;
            }

            _rentalDal.Add(entity);
            return new SuccessResult(Messages.RentalAdded);
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Delete(Rental entity) {
            _rentalDal.Delete(entity);
            return new SuccessResult(Messages.RentalDeleted);
        }

        public IDataResult<Rental> Get(Expression<Func<Rental, bool>> filter) {
            return new SuccessDataResult<Rental>(_rentalDal.Get(filter));
        }

        public IDataResult<List<Rental>> GetAll(Expression<Func<Rental, bool>> filter = null) {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(filter));
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Update(Rental entity) {
            _rentalDal.Update(entity);
            return new SuccessResult(Messages.RentalUpdated);
        }

        private IResult CheckIfRentedCarNotReturnedYet(Rental rental) {
            var result = _rentalDal.Get(r => r.CarId == rental.CarId && r.ReturnDate == DateTime.MinValue);
            if (result != null) {
                return new ErrorResult(Messages.RentedCarNotReturnedYet);
            }
            return new SuccessResult();
        }

        private IResult CheckIfRentalReturnDateIsBeforeRentDate(Rental rental) {
            if (rental.ReturnDate.Subtract(rental.RentDate).TotalDays < 0) {
                return new ErrorResult(Messages.RentalReturnDateIsBeforeRentDate);
            }
            return new SuccessResult();
        }
    }
}

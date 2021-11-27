using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
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
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Add(Rental rental) {
            var errorResult = BusinessEngine.Run(
                CheckIfRentedCarNotReturnedYet(rental),
                CheckIfRentalReturnDateIsBeforeRentDate(rental)
            );
            if (errorResult != null) {
                return errorResult;
            }

            _rentalDal.Add(rental);
            return new SuccessResult(Messages.RentalAdded);
        }

        [ValidationAspect(typeof(RentalValidator))]
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Delete(Rental rental) {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.RentalDeleted);
        }

        [CacheAspect]
        public IDataResult<List<Rental>> GetAll() {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        [CacheAspect]
        public IDataResult<List<RentalDetailDto>> GetAllRentalDetails() {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetAllRentalDetails());
        }

        [CacheAspect]
        public IDataResult<Rental> GetById(int id) {
            return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.Id == id));
        }

        [ValidationAspect(typeof(RentalValidator))]
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Update(Rental rental) {
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.RentalUpdated);
        }

        private IResult CheckIfRentedCarNotReturnedYet(Rental rental) {
            // DateTime.CompareTo() reference: https://docs.microsoft.com/en-us/dotnet/api/system.datetime.compareto?view=net-6.0

            // Return date should be earlier or today
            var result = _rentalDal.Get(r => r.CarId == rental.CarId && r.ReturnDate.CompareTo(DateTime.Today) <= 0);
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

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
                CheckIfCarAlreadyRentedInSpecifiedDate(rental),
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

        private IResult CheckIfCarAlreadyRentedInSpecifiedDate(Rental rental) {
            // DateTime.CompareTo() reference: https://docs.microsoft.com/en-us/dotnet/api/system.datetime.compareto?view=net-6.0

            // For every two timespan, there are 5 cases:
            // 1. timespan1 starts and ends before timespan2 starts.
            // 2. timespan1 starts before timespan2 starts, and ends inside timespan2.
            // 3. timespan1 starts and ends inside timespan2.
            // 4. timespan1 starts inside timespan2, and ends after timespan2 ends.
            // 5. timespan1 starts and ends after timespan2 ends.
            // timespan1 and timespan2 don't intersect only if case 1 or 5 happens.

            var result = _rentalDal.GetAll(r => r.CarId == rental.CarId);
            foreach (var loopRental in result) {
                if (!(                                                                                                                    // If not
                    ((rental.RentDate.CompareTo(loopRental.RentDate) < 0) && (rental.ReturnDate.CompareTo(loopRental.RentDate) < 0)) ||   // Case 1 or
                    ((rental.RentDate.CompareTo(loopRental.ReturnDate) > 0) && (rental.ReturnDate.CompareTo(loopRental.ReturnDate) > 0))  // Case 5,
                )) {                                                                                                                      // then
                    return new ErrorResult(Messages.CarAlreadyRentedInSpecifiedDate);                                                     // return error.
                }
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

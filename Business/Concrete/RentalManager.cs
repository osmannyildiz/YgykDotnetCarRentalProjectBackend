using Business.Abstract;
using Business.Constants;
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

        public IResult Add(Rental entity) {
            // Seçilen arabanın kiralandığı ama iade edilme tarihinin boş olduğu durumu sorguluyoruz
            var result = _rentalDal.Get(r => r.CarId == entity.CarId && r.ReturnDate == DateTime.MinValue);
            if (result != null) {
                return new ErrorResult(Messages.RentedCarNotReturnedYet);
            } else {
                _rentalDal.Add(entity);
                return new SuccessResult(Messages.RentalAdded);
            }
        }

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

        public IResult Update(Rental entity) {
            _rentalDal.Update(entity);
            return new SuccessResult(Messages.RentalUpdated);
        }
    }
}

using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Concrete {
    public class CarManager : ICarService {
        ICarDal _carDal;

        public CarManager(ICarDal carDal) {
            _carDal = carDal;
        }

        public IResult Add(Car entity) {
            if (entity.Description.Length < 2) {
                return new ErrorResult(Messages.CarDescriptionMustBeAtLeastTwoCharactersLong);
            } else if (entity.DailyPrice <= 0) {
                return new ErrorResult(Messages.CarDailyPriceMustBeBiggerThanZero);
            } else {
                _carDal.Add(entity);
                return new SuccessResult(Messages.CarAdded);
            }
        }

        public IResult Delete(Car entity) {
            _carDal.Delete(entity);
            return new SuccessResult(Messages.CarDeleted);
        }

        public IDataResult<Car> Get(Expression<Func<Car, bool>> filter) {
            return new SuccessDataResult<Car>(_carDal.Get(filter));
        }

        public IDataResult<List<Car>> GetAll(Expression<Func<Car, bool>> filter = null) {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(filter));
        }

        public IDataResult<List<Car>> GetByBrandId(int brandId) {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == brandId));
        }

        public IDataResult<List<Car>> GetByColorId(int colorId) {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == colorId));
        }

        public IDataResult<List<CarDetailDto>> GetCarsDetails() {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarsDetails());
        }

        public IResult Update(Car entity) {
            _carDal.Update(entity);
            return new SuccessResult(Messages.CarUpdated);
        }
    }
}

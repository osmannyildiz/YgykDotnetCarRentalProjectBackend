using Business.Abstract;
using Business.Aspects.Autofac.Auth;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
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

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get", "IRentalService.GetAllRentalDetails", "IRentalService.GetRentalDetail")]
        public IResult Add(Car car) {
            _carDal.Add(car);
            return new SuccessResult(Messages.CarAdded);
        }

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get", "IRentalService.GetAllRentalDetails", "IRentalService.GetRentalDetail")]
        public IResult Delete(Car car) {
            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);
        }

        [CacheAspect]
        public IDataResult<List<Car>> GetAll() {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll());
        }

        [CacheAspect]
        public IDataResult<List<Car>> GetAllByBrandId(int brandId) {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == brandId));
        }

        [CacheAspect]
        public IDataResult<List<Car>> GetAllByColorId(int colorId) {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == colorId));
        }

        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetAllCarDetails() {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetAllCarDetails());
        }

        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetAllCarDetailsByBrandId(int brandId) {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetAllCarDetails(cd => cd.BrandId == brandId));
        }

        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetAllCarDetailsByColorId(int colorId) {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetAllCarDetails(cd => cd.ColorId == colorId));
        }

        [CacheAspect]
        public IDataResult<List<CarDetailDto>> GetAllCarDetailsByBrandIdAndColorId(int brandId, int colorId) {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetAllCarDetails(cd => cd.BrandId == brandId && cd.ColorId == colorId));
        }

        [CacheAspect]
        public IDataResult<Car> GetById(int id) {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == id));
        }

        public IDataResult<CarDetailDto> GetCarDetailByCarId(int carId) {
            return new SuccessDataResult<CarDetailDto>(_carDal.GetCarDetail(cd => cd.CarId == carId));
        }

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get", "IRentalService.GetAllRentalDetails", "IRentalService.GetRentalDetail")]
        public IResult Update(Car car) {
            _carDal.Update(car);
            return new SuccessResult(Messages.CarUpdated);
        }
    }
}

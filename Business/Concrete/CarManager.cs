﻿using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
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

        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car) {
            _carDal.Add(car);
            return new SuccessResult(Messages.CarAdded);
        }

        [ValidationAspect(typeof(CarValidator))]
        public IResult Delete(Car car) {
            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);
        }

        public IDataResult<List<Car>> GetAll() {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll());
        }

        public IDataResult<List<Car>> GetAllByBrandId(int brandId) {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == brandId));
        }

        public IDataResult<List<Car>> GetAllByColorId(int colorId) {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == colorId));
        }

        public IDataResult<List<CarDetailDto>> GetAllCarDetails() {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetAllCarDetails());
        }

        public IDataResult<Car> GetById(int id) {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == id));
        }

        [ValidationAspect(typeof(CarValidator))]
        public IResult Update(Car car) {
            _carDal.Update(car);
            return new SuccessResult(Messages.CarUpdated);
        }
    }
}

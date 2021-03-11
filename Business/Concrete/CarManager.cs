using Business.Abstract;
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

        public void Add(Car entity) {
            if (entity.Description.Length < 2) {
                throw new Exception("Araba açıklaması en az 2 karakter uzunluğunda olmalıdır");
            } else if (entity.DailyPrice <= 0) {
                throw new Exception("Araba günlük fiyatı 0'dan büyük olmalıdır");
            } else {
                _carDal.Add(entity);
            }
        }

        public void Delete(Car entity) {
            _carDal.Delete(entity);
        }

        public Car Get(Expression<Func<Car, bool>> filter) {
            return _carDal.Get(filter);
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null) {
            return _carDal.GetAll(filter);
        }

        public List<Car> GetByBrandId(int brandId) {
            return _carDal.GetAll(c => c.BrandId == brandId);
        }

        public List<Car> GetByColorId(int colorId) {
            return _carDal.GetAll(c => c.ColorId == colorId);
        }

        public List<CarDetailDto> GetCarsDetails() {
            return _carDal.GetCarsDetails();
        }

        public void Update(Car entity) {
            _carDal.Update(entity);
        }
    }
}

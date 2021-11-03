using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory {
    public class InMemoryCarDal : ICarDal {
        List<Car> _cars;

        public InMemoryCarDal() {
            _cars = new List<Car> {
                new Car {Id=1, BrandId=1, ColorId=1, DailyPrice=150, ModelYear=2012, Description="2012 Hyundai Accent Era"},
                new Car {Id=2, BrandId=2, ColorId=2, DailyPrice=350, ModelYear=2015, Description="2015 Volkswagen Golf"},
                new Car {Id=3, BrandId=2, ColorId=1, DailyPrice=400, ModelYear=2014, Description="2014 Volkswagen Tiguan"}
            };
        }

        public void Add(Car entity) {
            _cars.Add(entity);
        }

        public void Delete(Car entity) {
            Car carToDelete = _cars.SingleOrDefault(c => c.Id == entity.Id);
            _cars.Remove(carToDelete);
        }

        public Car Get(Expression<Func<Car, bool>> filter) {
            return _cars.SingleOrDefault(filter.Compile());
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null) {
            if (filter == null) {
                return _cars;
            } else {
                return _cars.Where(filter.Compile()).ToList();
            }
        }

        public List<CarDetailDto> GetCarsDetails() {
            throw new NotImplementedException();
        }

        public void Update(Car entity) {
            Car carToUpdate = _cars.SingleOrDefault(c => c.Id == entity.Id);
            carToUpdate.BrandId = entity.BrandId;
            carToUpdate.ColorId = entity.ColorId;
            carToUpdate.Name = entity.Name;
            carToUpdate.ModelYear = entity.ModelYear;
            carToUpdate.DailyPrice = entity.DailyPrice;
            carToUpdate.Description = entity.Description;
        }
    }
}

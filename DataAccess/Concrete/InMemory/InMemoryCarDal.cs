using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Add(Car car) {
            _cars.Add(car);
        }

        public void Delete(Car car) {
            Car carToDelete = _cars.SingleOrDefault(c => c.Id == car.Id);
            _cars.Remove(carToDelete);
        }

        public List<Car> GetAll() {
            return _cars;
        }

        public Car GetById(int id) {
            return _cars.SingleOrDefault(c => c.Id == id);
        }

        public void Update(Car car) {
            Car carToUpdate = _cars.SingleOrDefault(c => c.Id == car.Id);
            carToUpdate.BrandId = car.BrandId;
            carToUpdate.ColorId = car.ColorId;
            carToUpdate.ModelYear = car.ModelYear;
            carToUpdate.DailyPrice = car.DailyPrice;
            carToUpdate.Description = car.Description;
        }
    }
}

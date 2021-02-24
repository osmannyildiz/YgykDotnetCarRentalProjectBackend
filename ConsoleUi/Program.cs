using Business.Concrete;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUi {
    class Program {
        static void Main(string[] args) {
            CarManager carManager = new CarManager(new InMemoryCarDal());
            carManager.Add(new Car { Id = 4, BrandId = 2, ColorId = 2, DailyPrice = 300, ModelYear = 2016, Description = "2016 Volkswagen Passat" });
            foreach (var car in carManager.GetAll()) {
                Console.WriteLine(car.Description);
            }
        }
    }
}

using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;

namespace ConsoleUi {
    class Program {
        static void Main(string[] args) {
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            CarManager carManager = new CarManager(new EfCarDal());
            ColorManager colorManager = new ColorManager(new EfColorDal());

            //AddInitialValuesToDb(brandManager, carManager, colorManager);
            //carManager.Add(new Car { BrandId = 2, ColorId = 2, DailyPrice = 300, ModelYear = 2016, Description = "2016 Volkswagen Passat" });

            foreach (var car in carManager.GetAll()) {
                //var brand = brandManager.Get(b => b.Id == car.BrandId);
                var color = colorManager.Get(c => c.Id == car.ColorId);
                Console.WriteLine("{0}, {1} - Günlük {2:F2} TL", car.Description, color.Name, car.DailyPrice);
            }
        }

        private static void AddInitialValuesToDb(BrandManager brandManager, CarManager carManager, ColorManager colorManager) {
            var brands = new List<Brand> {
                new Brand {Name="Hyundai"},
                new Brand {Name="Volkswagen"},
                new Brand {Name="Toyota"}
            };
            foreach (var brand in brands) {
                brandManager.Add(brand);
            }

            var colors = new List<Color> {
                new Color {Name="Beyaz"},
                new Color {Name="Gri"},
                new Color {Name="Kırmızı"}
            };
            foreach (var color in colors) {
                colorManager.Add(color);
            }

            var cars = new List<Car> {
                new Car {BrandId=1, ColorId=1, DailyPrice=150, ModelYear=2012, Description="2012 Hyundai Accent Era"},
                new Car {BrandId=2, ColorId=2, DailyPrice=350, ModelYear=2015, Description="2015 Volkswagen Golf"},
                new Car {BrandId=2, ColorId=1, DailyPrice=400, ModelYear=2014, Description="2014 Volkswagen Tiguan"}
            };
            foreach (var car in cars) {
                carManager.Add(car);
            }
        }
    }
}

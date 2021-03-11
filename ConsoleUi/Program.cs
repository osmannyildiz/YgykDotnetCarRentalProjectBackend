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
            //carManager.Add();

            foreach (var car in carManager.GetCarsDetails()) {
                Console.WriteLine("{0} {1} ({2}) - Günlük {3:F2} TL", car.BrandName, car.Name, car.ColorName, car.DailyPrice);
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
                new Car {BrandId=1, ColorId=1, DailyPrice=150, ModelYear=2012, Name="Accent Era", Description="Aile arabası"},
                new Car {BrandId=2, ColorId=2, DailyPrice=350, ModelYear=2015, Name="Golf", Description="Spor araba"},
                new Car {BrandId=2, ColorId=1, DailyPrice=400, ModelYear=2014, Name="Tiguan", Description="Piknik vb. için ideal"},
                new Car {BrandId=2, ColorId=2, DailyPrice=300, ModelYear=2016, Name="Passat", Description="Uzun yol için ideal"}
            };
            foreach (var car in cars) {
                carManager.Add(car);
            }
        }
    }
}

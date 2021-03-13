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
            CustomerManager customerManager = new CustomerManager(new EfCustomerDal());
            RentalManager rentalManager = new RentalManager(new EfRentalDal());
            UserManager userManager = new UserManager(new EfUserDal());

            //AddInitialValuesToDb(brandManager, carManager, colorManager, customerManager, rentalManager, userManager);

            var result = carManager.GetCarsDetails();
            if (result.Success) {
                foreach (var car in result.Data) {
                    Console.WriteLine("{0} {1} ({2}) - Günlük {3:F2} TL", car.BrandName, car.Name, car.ColorName, car.DailyPrice);
                }
            }

            var result2 = rentalManager.Add(new Rental { CarId = 3, CustomerId = 1, RentDate = new DateTime(2021, 3, 14) });
            Console.WriteLine(result2.Message);  // Success
            var result3 = rentalManager.Add(new Rental { CarId = 3, CustomerId = 2, RentDate = new DateTime(2021, 3, 14) });
            Console.WriteLine(result3.Message);  // Error


        }

        private static void AddInitialValuesToDb(
            BrandManager brandManager, 
            CarManager carManager, 
            ColorManager colorManager, 
            CustomerManager customerManager, 
            RentalManager rentalManager, 
            UserManager userManager
        ) {
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

            var users = new List<User> {
                new User {Id=1, FirstName="Engin", LastName="Demiroğ", Email="admin@kodlama.io", Password="cigkofte21"},
                new User {Id=2, FirstName="Osman Nuri", LastName="Yıldız", Email="iamosmannyildiz@gmail.com", Password="12ab34cd"}
            };
            foreach (var user in users) {
                userManager.Add(user);
            }

            var customers = new List<Customer> {
                new Customer {Id=1, UserId=1, CompanyName="Kodlama.io"},
                new Customer {Id=2, UserId=2, CompanyName="KTO Karatay Üniversitesi"}
            };
            foreach (var customer in customers) {
                customerManager.Add(customer);
            }

            var rentals = new List<Rental> {
                new Rental {Id=1, CarId=2, CustomerId=2, RentDate=new DateTime(2021, 3, 5), ReturnDate=new DateTime(2021, 3, 7)},
                new Rental {Id=2, CarId=2, CustomerId=2, RentDate=new DateTime(2021, 3, 13)}
            };
            foreach (var rental in rentals) {
                rentalManager.Add(rental);
            }
        }
    }
}

using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework {
    public class EfCarDal : EfEntityRepositoryBase<Car, YgykCarRentalProjectContext>, ICarDal {
        public List<CarDetailDto> GetAllCarDetails() {
            using (YgykCarRentalProjectContext context = new YgykCarRentalProjectContext()) {
                var result = from ca in context.Cars
                             join br in context.Brands
                             on ca.BrandId equals br.Id
                             join co in context.Colors
                             on ca.ColorId equals co.Id
                             select new CarDetailDto {
                                 Name = ca.Name,
                                 BrandName = br.Name,
                                 ColorName = co.Name,
                                 DailyPrice = ca.DailyPrice
                             };
                return result.ToList();
            }
        }
    }
}

using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework {
    public class EfCarDal : EfEntityRepositoryBase<Car, YgykCarRentalProjectContext>, ICarDal {
        public List<CarDetailDto> GetAllCarDetails(Expression<Func<CarDetailDto, bool>> filter = null) {
            using (YgykCarRentalProjectContext context = new YgykCarRentalProjectContext()) {
                var result = from ca in context.Cars
                             join br in context.Brands
                             on ca.BrandId equals br.Id
                             join co in context.Colors
                             on ca.ColorId equals co.Id
                             select new CarDetailDto {
                                 CarId = ca.Id,
                                 CarName = ca.Name,
                                 BrandId = br.Id,
                                 BrandName = br.Name,
                                 ColorId = co.Id,
                                 ColorName = co.Name,
                                 CarDailyPrice = ca.DailyPrice
                             };

                if (filter != null) {
                    var compiledFilter = filter.Compile();
                    return result.Where(compiledFilter).ToList();
                } else {
                    return result.ToList();
                }
            }
        }

        public CarDetailDto GetCarDetail(Expression<Func<CarDetailDto, bool>> filter = null) {
            return GetAllCarDetails(filter).First<CarDetailDto>();
        }
    }
}

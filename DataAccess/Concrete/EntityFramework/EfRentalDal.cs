using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework {
    public class EfRentalDal : EfEntityRepositoryBase<Rental, YgykCarRentalProjectContext>, IRentalDal {
        public List<RentalDetailDto> GetAllRentalDetails() {
            using (YgykCarRentalProjectContext context = new YgykCarRentalProjectContext()) {
                var result = from re in context.Rentals
                             join ca in context.Cars
                             on re.CarId equals ca.Id
                             join br in context.Brands
                             on ca.BrandId equals br.Id
                             join cu in context.Customers
                             on re.CustomerId equals cu.Id
                             join us in context.Users
                             on cu.UserId equals us.Id
                             select new RentalDetailDto {
                                 CarName = ca.Name,
                                 CarBrandName = br.Name,
                                 CustomerUserFullName = $"{us.FirstName} {us.LastName}",
                                 CustomerCompanyName = cu.CompanyName,
                                 RentDate = re.RentDate,
                                 ReturnDate = re.ReturnDate
                             };
                return result.ToList();
            }
        }
    }
}

using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory {
    public class InMemoryRentalDal : IRentalDal {
        List<Rental> _rentals;

        public InMemoryRentalDal() {
            _rentals = new List<Rental> {
                new Rental {Id=1, CarId=2, CustomerId=2, RentDate=new DateTime(2021, 3, 5), ReturnDate=new DateTime(2021, 3, 7)},
                new Rental {Id=2, CarId=2, CustomerId=2, RentDate=new DateTime(2021, 3, 13)}
            };
        }

        public void Add(Rental entity) {
            _rentals.Add(entity);
        }

        public void Delete(Rental entity) {
            Rental rentalToDelete = _rentals.SingleOrDefault(r => r.Id == entity.Id);
            _rentals.Remove(rentalToDelete);
        }

        public Rental Get(Expression<Func<Rental, bool>> filter) {
            return _rentals.SingleOrDefault(filter.Compile());
        }

        public List<Rental> GetAll(Expression<Func<Rental, bool>> filter = null) {
            if (filter == null) {
                return _rentals;
            } else {
                return _rentals.Where(filter.Compile()).ToList();
            }
        }

        public void Update(Rental entity) {
            Rental rentalToUpdate = _rentals.SingleOrDefault(r => r.Id == entity.Id);
            rentalToUpdate.Id = entity.Id;
            rentalToUpdate.CarId = entity.CarId;
            rentalToUpdate.CustomerId = entity.CustomerId;
            rentalToUpdate.RentDate = entity.RentDate;
            rentalToUpdate.ReturnDate = entity.ReturnDate;
        }
    }
}

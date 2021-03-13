using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory {
    public class InMemoryCustomerDal : ICustomerDal {
        List<Customer> _customers;

        public InMemoryCustomerDal() {
            _customers = new List<Customer> {
                new Customer {Id=1, UserId=1, CompanyName="Kodlama.io"},
                new Customer {Id=2, UserId=2, CompanyName="KTO Karatay Üniversitesi"}
            };
        }

        public void Add(Customer entity) {
            _customers.Add(entity);
        }

        public void Delete(Customer entity) {
            Customer customerToDelete = _customers.SingleOrDefault(c => c.Id == entity.Id);
            _customers.Remove(customerToDelete);
        }

        public Customer Get(Expression<Func<Customer, bool>> filter) {
            return _customers.SingleOrDefault(filter.Compile());
        }

        public List<Customer> GetAll(Expression<Func<Customer, bool>> filter = null) {
            if (filter == null) {
                return _customers;
            } else {
                return _customers.Where(filter.Compile()).ToList();
            }
        }

        public void Update(Customer entity) {
            Customer customerToUpdate = _customers.SingleOrDefault(c => c.Id == entity.Id);
            customerToUpdate.Id = entity.Id;
            customerToUpdate.UserId = entity.UserId;
            customerToUpdate.CompanyName = entity.CompanyName;
        }
    }
}

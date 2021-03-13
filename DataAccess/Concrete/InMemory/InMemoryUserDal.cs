using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory {
    public class InMemoryUserDal : IUserDal {
        List<User> _users;

        public InMemoryUserDal() {
            _users = new List<User> {
                new User {Id=1, FirstName="Engin", LastName="Demiroğ", Email="admin@kodlama.io", Password="cigkofte21"},
                new User {Id=2, FirstName="Osman Nuri", LastName="Yıldız", Email="iamosmannyildiz@gmail.com", Password="12ab34cd"}
            };
        }

        public void Add(User entity) {
            _users.Add(entity);
        }

        public void Delete(User entity) {
            User userToDelete = _users.SingleOrDefault(u => u.Id == entity.Id);
            _users.Remove(userToDelete);
        }

        public User Get(Expression<Func<User, bool>> filter) {
            return _users.SingleOrDefault(filter.Compile());
        }

        public List<User> GetAll(Expression<Func<User, bool>> filter = null) {
            if (filter == null) {
                return _users;
            } else {
                return _users.Where(filter.Compile()).ToList();
            }
        }

        public void Update(User entity) {
            User userToUpdate = _users.SingleOrDefault(u => u.Id == entity.Id);
            userToUpdate.Id = entity.Id;
            userToUpdate.FirstName = entity.FirstName;
            userToUpdate.LastName = entity.LastName;
            userToUpdate.Email = entity.Email;
            userToUpdate.Password = entity.Password;
        }
    }
}

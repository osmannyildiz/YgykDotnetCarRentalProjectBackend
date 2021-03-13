using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory {
    public class InMemoryBrandDal : IBrandDal {
        List<Brand> _brands;

        public InMemoryBrandDal() {
            _brands = new List<Brand> {
                new Brand {Id=1, Name="Hyundai"},
                new Brand {Id=2, Name="Volkswagen"},
                new Brand {Id=3, Name="Toyota"}
            };
        }

        public void Add(Brand entity) {
            _brands.Add(entity);
        }

        public void Delete(Brand entity) {
            Brand brandToDelete = _brands.SingleOrDefault(b => b.Id == entity.Id);
            _brands.Remove(brandToDelete);
        }

        public Brand Get(Expression<Func<Brand, bool>> filter) {
            return _brands.SingleOrDefault(filter.Compile());
        }

        public List<Brand> GetAll(Expression<Func<Brand, bool>> filter = null) {
            if (filter == null) {
                return _brands;
            } else {
                return _brands.Where(filter.Compile()).ToList();
            }
        }

        public void Update(Brand entity) {
            Brand brandToUpdate = _brands.SingleOrDefault(b => b.Id == entity.Id);
            brandToUpdate.Id = entity.Id;
            brandToUpdate.Name = entity.Name;
        }
    }
}

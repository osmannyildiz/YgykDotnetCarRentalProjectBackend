using DataAccess.Abstract;
using Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework {
    public class EfEntityRepository<T> : IEntityRepository<T> where T : class, IEntity, new() {
        public void Add(T entity) {
            using (YgykCarRentalProjectContext context = new YgykCarRentalProjectContext()) {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(T entity) {
            using (YgykCarRentalProjectContext context = new YgykCarRentalProjectContext()) {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public T Get(Expression<Func<T, bool>> filter) {
            using (YgykCarRentalProjectContext context = new YgykCarRentalProjectContext()) {
                return context.Set<T>().SingleOrDefault(filter);
            }
        }

        public List<T> GetAll(Expression<Func<T, bool>> filter = null) {
            using (YgykCarRentalProjectContext context = new YgykCarRentalProjectContext()) {
                if (filter == null) {
                    return context.Set<T>().ToList();
                } else {
                    return context.Set<T>().Where(filter).ToList();
                }
            }
        }

        public void Update(T entity) {
            using (YgykCarRentalProjectContext context = new YgykCarRentalProjectContext()) {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}

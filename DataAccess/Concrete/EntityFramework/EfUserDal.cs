using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.Dtos;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework {
    public class EfUserDal : EfEntityRepositoryBase<User, YgykCarRentalProjectContext>, IUserDal {
        // NOTE I'm not sure if it's okay for a DAL to depend on another DAL
        ICustomerDal _customerDal;

        public EfUserDal(ICustomerDal customerDal) {
            _customerDal = customerDal;
        }

        // NOTE Maybe this should take an OperationClaim instead of a string?
        public void AddOperationClaim(User user, string operationClaimName) {
            using (var context = new YgykCarRentalProjectContext()) {
                var operationClaim = context.Set<OperationClaim>().SingleOrDefault(oc => oc.Name == operationClaimName);
                if (operationClaim != null) {
                    // FIXME Duplicated code of EfEntityRepositoryBase.Add
                    var entity = new UserOperationClaim {
                        UserId = user.Id,
                        OperationClaimId = operationClaim.Id
                    };
                    var addedEntity = context.Entry(entity);
                    addedEntity.State = EntityState.Added;
                    context.SaveChanges();
                }
                // FIXME It will silently fail if the given operation claim doesn't exist, which is bad
            }
        }

        public List<OperationClaim> GetOperationClaims(User user) {
            using (var context = new YgykCarRentalProjectContext()) {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                             on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim {
                                 Id = operationClaim.Id,
                                 Name = operationClaim.Name
                             };
                return result.ToList();
            }
        }

        public UserInfoDto GetUserInfo(Expression<Func<User, bool>> filter) {
            var user = Get(filter);
            var customer = _customerDal.Get(c => c.UserId == user.Id);
            var result = new UserInfoDto {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                CustomerId = customer.Id,
                CustomerCompanyName = customer.CompanyName
            };
            return result;
        }
    }
}

using Core.DataAccess;
using Core.Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract {
    public interface IUserDal : IEntityRepository<User> {
        List<OperationClaim> GetOperationClaims(User user);
        void AddOperationClaim(User user, string operationClaimName);
        UserInfoDto GetUserInfo(Expression<Func<User, bool>> filter);
    }
}

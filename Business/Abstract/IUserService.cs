using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract {
    public interface IUserService {
        IDataResult<List<User>> GetAll();
        IDataResult<User> GetById(int id);
        IDataResult<User> GetByEmail(string email);
        IResult Add(User user);
        IResult Update(User user);
        IResult Delete(User user);
        IDataResult<List<OperationClaim>> GetOperationClaims(User user);
        IResult AddOperationClaim(User user, string operationClaimName);
        IDataResult<UserInfoDto> GetUserInfoByUserId(int userId);
        IDataResult<UserInfoDto> GetUserInfoByEmail(string email);
        IResult UpdateUserInfo(UserInfoDto userInfoDto);
    }
}

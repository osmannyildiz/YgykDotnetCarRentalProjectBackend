using Core.Entities;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Abstract {
    public interface IEntityService<T> where T : class, IEntity, new() {
        IDataResult<T> Get(Expression<Func<T, bool>> filter);
        IDataResult<List<T>> GetAll(Expression<Func<T, bool>> filter = null);
        IResult Add(T entity);
        IResult Update(T entity);
        IResult Delete(T entity);
    }
}

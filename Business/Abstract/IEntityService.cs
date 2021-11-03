using Core.Entities;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Abstract {
    public interface IEntityService<TEntity, TAdd, TUpdate>
        where TEntity : class, IEntity, new()
        where TAdd : class, new()
    {
        IDataResult<TEntity> Get(Expression<Func<TEntity, bool>> filter);
        IDataResult<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter = null);
        IResult Add(TAdd entityOrDto);
        IResult Update(TUpdate entityOrDto);
        IResult Delete(TEntity entity);
    }
}

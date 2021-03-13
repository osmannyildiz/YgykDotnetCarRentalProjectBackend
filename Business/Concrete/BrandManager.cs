using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Concrete {
    public class BrandManager : IBrandService {
        IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal) {
            _brandDal = brandDal;
        }

        public IResult Add(Brand entity) {
            _brandDal.Add(entity);
            return new SuccessResult(Messages.BrandAdded);
        }

        public IResult Delete(Brand entity) {
            _brandDal.Delete(entity);
            return new SuccessResult(Messages.BrandDeleted);
        }

        public IDataResult<Brand> Get(Expression<Func<Brand, bool>> filter) {
            return new SuccessDataResult<Brand>(_brandDal.Get(filter));
        }

        public IDataResult<List<Brand>> GetAll(Expression<Func<Brand, bool>> filter = null) {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(filter));
        }

        public IResult Update(Brand entity) {
            _brandDal.Update(entity);
            return new SuccessResult(Messages.BrandUpdated);
        }
    }
}

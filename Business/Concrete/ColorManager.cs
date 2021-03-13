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
    public class ColorManager : IColorService {
        IColorDal _colorDal;

        public ColorManager(IColorDal colorDal) {
            _colorDal = colorDal;
        }

        public IResult Add(Color entity) {
            _colorDal.Add(entity);
            return new SuccessResult(Messages.ColorAdded);
        }

        public IResult Delete(Color entity) {
            _colorDal.Delete(entity);
            return new SuccessResult(Messages.ColorDeleted);
        }

        public IDataResult<Color> Get(Expression<Func<Color, bool>> filter) {
            return new SuccessDataResult<Color>(_colorDal.Get(filter));
        }

        public IDataResult<List<Color>> GetAll(Expression<Func<Color, bool>> filter = null) {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll(filter));
        }

        public IResult Update(Color entity) {
            _colorDal.Update(entity);
            return new SuccessResult(Messages.ColorUpdated);
        }
    }
}

using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract {
    public interface ICarService : IEntityService<Car> {
        List<Car> GetByBrandId(int brandId);
        List<Car> GetByColorId(int colorId);
    }
}

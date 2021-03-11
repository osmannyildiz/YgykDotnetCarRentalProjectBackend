using Core.DataAccess;
using Entities.Concrete;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract {
    public interface ICarDal : IEntityRepository<Car> {
        List<CarDetailDto> GetCarsDetails();
    }
}

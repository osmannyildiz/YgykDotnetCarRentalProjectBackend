using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract {
    public interface ICarService {
        IDataResult<List<Car>> GetAll();
        IDataResult<List<Car>> GetAllByBrandId(int brandId);
        IDataResult<List<Car>> GetAllByColorId(int colorId);
        IDataResult<Car> GetById(int id);
        IResult Add(Car car);
        IResult Update(Car car);
        IResult Delete(Car car);
        IDataResult<List<CarDetailDto>> GetAllCarDetails();
        IDataResult<List<CarDetailDto>> GetAllCarDetailsByBrandId(int brandId);
        IDataResult<List<CarDetailDto>> GetAllCarDetailsByColorId(int colorId);
        IDataResult<CarDetailDto> GetCarDetailByCarId(int carId);
    }
}

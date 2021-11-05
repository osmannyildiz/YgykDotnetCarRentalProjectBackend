using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract {
    public interface ICarImageService {
        IDataResult<List<CarImage>> GetAll();
        IDataResult<List<CarImage>> GetAllByCarId(int carId);
        IDataResult<CarImage> GetById(int id);
        IResult Add(CarImageAddDto carImageAddDto);
        IResult Update(CarImageUpdateDto carImageUpdateDto);
        IResult Delete(CarImage carImage);
    }
}

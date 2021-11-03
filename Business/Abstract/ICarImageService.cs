using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract {
    public interface ICarImageService : IEntityService<CarImage, CarImageAddDto, CarImageUpdateDto> {
    }
}

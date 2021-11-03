using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.FileSystem;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Text;

namespace Business.Concrete {
    public class CarImageManager : ICarImageService {
        ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal) {
            _carImageDal = carImageDal;
        }

        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Add(CarImageAddDto dto) {
            var result1 = GetAll(ci => ci.CarId == dto.CarId);
            if (result1.Data != null && result1.Data.Count >= Values.MaxCountOfImagesPerCar) {
                return new ErrorResult(Messages.CarHasMaxCountOfImages);
            }

            // TODO Do file validation
            // TODO Detect file extension better, instead of relying on file name
            string imageFilePath = "Files/CarImages/" + Guid.NewGuid().ToString() + Path.GetExtension(dto.ImageFile.FileName);
            FileSystemTool.SaveFormFile(dto.ImageFile, imageFilePath);
            var entity = new CarImage {
                CarId = dto.CarId,
                ImageFilePath = imageFilePath,
                UploadDate = DateTime.Today
            };
            _carImageDal.Add(entity);
            return new SuccessResult(Messages.CarImageAdded);
        }

        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Delete(CarImage entity) {
            FileSystemTool.DeleteFileIfExists(entity.ImageFilePath);
            _carImageDal.Delete(entity);
            return new SuccessResult(Messages.CarImageDeleted);
        }

        public IDataResult<CarImage> Get(Expression<Func<CarImage, bool>> filter) {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(filter));
        }

        public IDataResult<List<CarImage>> GetAll(Expression<Func<CarImage, bool>> filter = null) {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(filter));
        }

        //[ValidationAspect(typeof(CarImageUpdateDtoValidator))]
        public IResult Update(CarImageUpdateDto dto) {
            var result1 = Get(ci => ci.Id == dto.Id);
            if (!result1.Success || result1.Data == null) {
                return new ErrorResult(Messages.NotFound);
            }
            var carImage = result1.Data;

            FileSystemTool.DeleteFileIfExists(carImage.ImageFilePath);
            // TODO Do file validation
            // TODO Detect file extension better, instead of relying on file name
            string imageFilePath = "Files/CarImages/" + Guid.NewGuid().ToString() + Path.GetExtension(dto.ImageFile.FileName);
            FileSystemTool.SaveFormFile(dto.ImageFile, imageFilePath);
            carImage.ImageFilePath = imageFilePath;

            _carImageDal.Update(carImage);
            return new SuccessResult(Messages.CarImageUpdated);
        }
    }
}

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
using System.Text;

namespace Business.Concrete {
    public class CarImageManager : ICarImageService {
        ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal) {
            _carImageDal = carImageDal;
        }

        [ValidationAspect(typeof(CarImageAddDtoValidator))]
        public IResult Add(CarImageAddDto carImageAddDto) {
            var result1 = GetAllByCarId(carImageAddDto.CarId);
            if (result1.Data != null && result1.Data.Count >= Values.MaxCountOfImagesPerCar) {
                return new ErrorResult(Messages.CarHasMaxCountOfImages);
            }

            // TODO Do file validation
            // TODO Detect file extension better, instead of relying on file name
            string imageFilePath = "Files/CarImages/" + Guid.NewGuid().ToString() + Path.GetExtension(carImageAddDto.ImageFile.FileName);
            FileSystemTool.SaveFormFile(carImageAddDto.ImageFile, imageFilePath);
            var entity = new CarImage {
                CarId = carImageAddDto.CarId,
                ImageFilePath = imageFilePath,
                UploadDate = DateTime.Today
            };
            _carImageDal.Add(entity);
            return new SuccessResult(Messages.CarImageAdded);
        }

        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Delete(CarImage carImage) {
            FileSystemTool.DeleteFileIfExists(carImage.ImageFilePath);
            _carImageDal.Delete(carImage);
            return new SuccessResult(Messages.CarImageDeleted);
        }

        public IDataResult<List<CarImage>> GetAll() {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        public IDataResult<List<CarImage>> GetAllByCarId(int carId) {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(ci => ci.CarId == carId));
        }

        public IDataResult<CarImage> GetById(int id) {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(ci => ci.Id == id));
        }

        //[ValidationAspect(typeof(CarImageUpdateDtoValidator))]
        public IResult Update(CarImageUpdateDto carImageUpdateDto) {
            var result1 = GetById(carImageUpdateDto.Id);
            if (!result1.Success || result1.Data == null) {
                return new ErrorResult(Messages.NotFound);
            }
            var carImage = result1.Data;

            FileSystemTool.DeleteFileIfExists(carImage.ImageFilePath);
            // TODO Do file validation
            // TODO Detect file extension better, instead of relying on file name
            string imageFilePath = "Files/CarImages/" + Guid.NewGuid().ToString() + Path.GetExtension(carImageUpdateDto.ImageFile.FileName);
            FileSystemTool.SaveFormFile(carImageUpdateDto.ImageFile, imageFilePath);
            carImage.ImageFilePath = imageFilePath;

            _carImageDal.Update(carImage);
            return new SuccessResult(Messages.CarImageUpdated);
        }
    }
}

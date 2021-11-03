using Business.Abstract;
using Core.Utilities.FileSystem;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CarImagesController : ControllerBase {
        ICarImageService _carImageService;

        public CarImagesController(ICarImageService carImageService) {
            _carImageService = carImageService;
        }

        [HttpGet("getAllByCarId")]
        public IActionResult GetAllByCarId(int carId) {
            var result = _carImageService.GetAll(ci => ci.CarId == carId);
            if (!result.Success) {
                return BadRequest(result);
            }

            if (result.Data.Count == 0) {
                result.Data.Add(new CarImage {
                    CarId = carId,
                    ImageFilePath = "Files/CarImages/no-image.png"
                });
            }
            
            return Ok(result);
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id) {
            var result = _carImageService.Get(ci => ci.Id == id);
            if (!result.Success) {
                return BadRequest(result);
            }
            
            return Ok(result);
        }

        [HttpPost("add")]
        public IActionResult Add([FromForm] CarImageAddDto carImageAddDto) {
            var result = _carImageService.Add(carImageAddDto);
            if (!result.Success) {
                return BadRequest(result);
            }
            
            return Ok(result);
        }

        [HttpPost("update")]
        public IActionResult Update([FromForm] CarImageUpdateDto carImageUpdateDto) {
            var result = _carImageService.Update(carImageUpdateDto);
            if (!result.Success) {
                return BadRequest(result);
            }
            
            return Ok(result);
        }

        [HttpDelete("deleteById")]
        public IActionResult DeleteById(int id) {
            var result1 = _carImageService.Get(ci => ci.Id == id);
            if (!result1.Success) {
                return BadRequest(result1);
            }
            if (result1.Data == null) {
                return NotFound();
            }
            var carImage = result1.Data;

            var result2 = _carImageService.Delete(carImage);
            if (!result2.Success) {
                return BadRequest(result2);
            }
            
            return Ok(result2);
        }
    }
}

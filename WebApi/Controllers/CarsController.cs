using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase {
        ICarService _carService;

        public CarsController(ICarService carService) {
            _carService = carService;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll() {
            var result = _carService.GetAll();
            if (result.Success) {
                return Ok(result);
            } else {
                return BadRequest(result);
            }
        }

        [HttpGet("getAllCarDetails")]
        public IActionResult GetAllCarDetails() {
            var result = _carService.GetAllCarDetails();
            if (result.Success) {
                return Ok(result);
            } else {
                return BadRequest(result);
            }
        }

        [HttpGet("getAllCarDetailsByBrandId")]
        public IActionResult GetAllCarDetailsByBrandId(int brandId) {
            var result = _carService.GetAllCarDetailsByBrandId(brandId);
            if (result.Success) {
                return Ok(result);
            } else {
                return BadRequest(result);
            }
        }

        [HttpGet("getAllCarDetailsByColorId")]
        public IActionResult GetAllCarDetailsByColorId(int colorId) {
            var result = _carService.GetAllCarDetailsByColorId(colorId);
            if (result.Success) {
                return Ok(result);
            } else {
                return BadRequest(result);
            }
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id) {
            var result = _carService.GetById(id);
            if (result.Success) {
                return Ok(result);
            } else {
                return BadRequest(result);
            }
        }

        [HttpGet("getCarDetailByCarId")]
        public IActionResult GetCarDetailByCarId(int carId) {
            var result = _carService.GetCarDetailByCarId(carId);
            if (result.Success) {
                return Ok(result);
            } else {
                return BadRequest(result);
            }
        }

        [HttpPost("add")]
        public IActionResult Add(Car car) {
            var result = _carService.Add(car);
            if (result.Success) {
                return Ok(result);
            } else {
                return BadRequest(result);
            }
        }

        [HttpPost("update")]
        public IActionResult Update(Car car) {
            var result = _carService.Update(car);
            if (result.Success) {
                return Ok(result);
            } else {
                return BadRequest(result);
            }
        }

        [HttpDelete("deleteById")]
        public IActionResult DeleteById(int id) {
            var result1 = _carService.GetById(id);
            var car = result1.Data;
            if (!result1.Success || car == null) {
                return BadRequest(result1);
            }

            var result2 = _carService.Delete(car);
            if (result2.Success) {
                return Ok(result2);
            } else {
                return BadRequest(result2);
            }
        }
    }
}

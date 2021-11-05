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
    public class RentalsController : ControllerBase {
        IRentalService _rentalService;

        public RentalsController(IRentalService rentalService) {
            _rentalService = rentalService;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll() {
            var result = _rentalService.GetAll();
            if (result.Success) {
                return Ok(result);
            } else {
                return BadRequest(result);
            }
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id) {
            var result = _rentalService.GetById(id);
            if (result.Success) {
                return Ok(result);
            } else {
                return BadRequest(result);
            }
        }

        [HttpPost("add")]
        public IActionResult Add(Rental rental) {
            var result = _rentalService.Add(rental);
            if (result.Success) {
                return Ok(result);
            } else {
                return BadRequest(result);
            }
        }

        [HttpPost("update")]
        public IActionResult Update(Rental rental) {
            var result = _rentalService.Update(rental);
            if (result.Success) {
                return Ok(result);
            } else {
                return BadRequest(result);
            }
        }

        [HttpDelete("deleteById")]
        public IActionResult DeleteById(int id) {
            var result1 = _rentalService.GetById(id);
            var rental = result1.Data;
            if (!result1.Success || rental == null) {
                return BadRequest(result1);
            }

            var result2 = _rentalService.Delete(rental);
            if (result2.Success) {
                return Ok(result2);
            } else {
                return BadRequest(result2);
            }
        }
    }
}

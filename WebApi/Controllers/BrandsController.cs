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
    public class BrandsController : ControllerBase {
        IBrandService _brandService;

        public BrandsController(IBrandService brandService) {
            _brandService = brandService;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll() {
            var result = _brandService.GetAll();
            if (result.Success) {
                return Ok(result);
            } else {
                return BadRequest(result);
            }
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id) {
            var result = _brandService.Get(b => b.Id == id);
            if (result.Success) {
                return Ok(result);
            } else {
                return BadRequest(result);
            }
        }

        [HttpPost("add")]
        public IActionResult Add(Brand brand) {
            var result = _brandService.Add(brand);
            if (result.Success) {
                return Ok(result);
            } else {
                return BadRequest(result);
            }
        }

        [HttpPost("update")]
        public IActionResult Update(Brand brand) {
            var result = _brandService.Update(brand);
            if (result.Success) {
                return Ok(result);
            } else {
                return BadRequest(result);
            }
        }

        [HttpDelete("deleteById")]
        public IActionResult DeleteById(int id) {
            var result1 = _brandService.Get(b => b.Id == id);
            var brand = result1.Data;
            if (!result1.Success || brand == null) {
                return BadRequest(result1);
            }

            var result2 = _brandService.Delete(brand);
            if (result2.Success) {
                return Ok(result2);
            } else {
                return BadRequest(result2);
            }
        }
    }
}

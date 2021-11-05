using Business.Abstract;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase {
        IUserService _userService;

        public UsersController(IUserService userService) {
            _userService = userService;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll() {
            var result = _userService.GetAll();
            if (result.Success) {
                return Ok(result);
            } else {
                return BadRequest(result);
            }
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id) {
            var result = _userService.GetById(id);
            if (result.Success) {
                return Ok(result);
            } else {
                return BadRequest(result);
            }
        }

        [HttpPost("add")]
        public IActionResult Add(User user) {
            var result = _userService.Add(user);
            if (result.Success) {
                return Ok(result);
            } else {
                return BadRequest(result);
            }
        }

        [HttpPost("update")]
        public IActionResult Update(User user) {
            var result = _userService.Update(user);
            if (result.Success) {
                return Ok(result);
            } else {
                return BadRequest(result);
            }
        }

        [HttpDelete("deleteById")]
        public IActionResult DeleteById(int id) {
            var result1 = _userService.GetById(id);
            var user = result1.Data;
            if (!result1.Success || user == null) {
                return BadRequest(result1);
            }

            var result2 = _userService.Delete(user);
            if (result2.Success) {
                return Ok(result2);
            } else {
                return BadRequest(result2);
            }
        }
    }
}

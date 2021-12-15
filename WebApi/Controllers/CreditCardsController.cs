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
    public class CreditCardsController : ControllerBase {
        ICreditCardService _creditCardService;

        public CreditCardsController(ICreditCardService creditCardService) {
            _creditCardService = creditCardService;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll() {
            var result = _creditCardService.GetAll();
            if (result.Success) {
                return Ok(result);
            } else {
                return BadRequest(result);
            }
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id) {
            var result = _creditCardService.GetById(id);
            if (result.Success) {
                return Ok(result);
            } else {
                return BadRequest(result);
            }
        }

        [HttpGet("getByUserId")]
        public IActionResult GetByUserId(int userId) {
            var result = _creditCardService.GetByUserId(userId);
            if (result.Success) {
                return Ok(result);
            } else {
                return BadRequest(result);
            }
        }

        [HttpPost("add")]
        public IActionResult Add(CreditCard creditCard) {
            var result = _creditCardService.Add(creditCard);
            if (result.Success) {
                return Ok(result);
            } else {
                return BadRequest(result);
            }
        }

        [HttpPost("update")]
        public IActionResult Update(CreditCard creditCard) {
            var result = _creditCardService.Update(creditCard);
            if (result.Success) {
                return Ok(result);
            } else {
                return BadRequest(result);
            }
        }

        [HttpDelete("deleteById")]
        public IActionResult DeleteById(int id) {
            var result1 = _creditCardService.GetById(id);
            var creditCard = result1.Data;
            if (!result1.Success || creditCard == null) {
                return BadRequest(result1);
            }

            var result2 = _creditCardService.Delete(creditCard);
            if (result2.Success) {
                return Ok(result2);
            } else {
                return BadRequest(result2);
            }
        }
    }
}

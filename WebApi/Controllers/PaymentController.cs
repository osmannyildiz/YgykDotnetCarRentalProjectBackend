using Business.Abstract;
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
    public class PaymentController : ControllerBase {
        IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService) {
            _paymentService = paymentService;
        }

        [HttpPost("process")]
        public IActionResult Process(PaymentFormDataDto formDataDto) {
            var result = _paymentService.Process(formDataDto);
            if (result.Success) {
                return Ok(result);
            } else {
                return BadRequest(result);
            }
        }
    }
}

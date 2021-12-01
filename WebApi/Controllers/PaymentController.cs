using Business.Abstract;
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
        public IActionResult Process([FromForm] string creditCardNumber, [FromForm] string creditCardExpiry, [FromForm] string creditCardCvc) {
            var result = _paymentService.Process(creditCardNumber, creditCardExpiry, creditCardCvc);
            if (result.Success) {
                return Ok(result);
            } else {
                return BadRequest(result);
            }
        }
    }
}

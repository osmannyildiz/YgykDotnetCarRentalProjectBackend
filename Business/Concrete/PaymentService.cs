using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete {
    public class PaymentService : IPaymentService {
        public IResult Process(PaymentFormDataDto paymentInfo) {
            return new SuccessResult(Messages.PaymentSuccessful);
            //return new ErrorResult(Messages.PaymentFailed);
        }
    }
}

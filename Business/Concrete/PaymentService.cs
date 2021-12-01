using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete {
    public class PaymentService : IPaymentService {
        public IResult Process(string creditCardNumber, string creditCardExpiry, string creditCardCvc) {
            return new SuccessResult(Messages.PaymentSuccessful);
        }
    }
}

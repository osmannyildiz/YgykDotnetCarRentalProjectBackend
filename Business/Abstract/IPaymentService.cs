using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract {
    public interface IPaymentService {
        IResult Process(string creditCardNumber, string creditCardExpiry, string creditCardCvc);
    }
}

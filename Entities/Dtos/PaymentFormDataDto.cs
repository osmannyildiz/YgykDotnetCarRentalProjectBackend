using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos {
    public class PaymentFormDataDto : IDto {
        public double Amount { get; set; }
        public CreditCard CreditCard { get; set; }
    }
}

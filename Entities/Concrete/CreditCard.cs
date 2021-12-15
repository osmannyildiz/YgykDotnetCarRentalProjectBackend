using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete {
    public class CreditCard : IEntity {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CreditCardNumber { get; set; }
        public string CreditCardExpiryMonth { get; set; }
        public string CreditCardExpiryYear { get; set; }
        public string CreditCardCvc { get; set; }
    }
}

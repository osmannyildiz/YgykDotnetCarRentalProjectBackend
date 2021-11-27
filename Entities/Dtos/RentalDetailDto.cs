using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos {
    public class RentalDetailDto : IDto {
        public string CarName { get; set; }
        public string CarBrandName { get; set; }
        public string CustomerUserFullName { get; set; }
        public string CustomerCompanyName { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}

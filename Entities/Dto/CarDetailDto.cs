﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dto {
    public class CarDetailDto : IDto {
        public string Name { get; set; }
        public string BrandName { get; set; }
        public string ColorName { get; set; }
        public decimal DailyPrice { get; set; }
    }
}

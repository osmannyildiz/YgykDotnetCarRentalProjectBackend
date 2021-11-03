using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos {
    public class CarImageAddDto : IDto {
        public int CarId { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}

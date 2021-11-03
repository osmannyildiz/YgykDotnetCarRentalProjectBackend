using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos {
    public class CarImageUpdateDto : IDto {
        public int Id { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}

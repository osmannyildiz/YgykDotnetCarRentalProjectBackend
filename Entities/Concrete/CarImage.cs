﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete {
    public class CarImage : IEntity {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string ImageFilePath { get; set; }
        public DateTime UploadDate { get; set; }
    }
}

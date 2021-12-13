using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos {
    public class ChangePasswordFormDataDto : IDto {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}

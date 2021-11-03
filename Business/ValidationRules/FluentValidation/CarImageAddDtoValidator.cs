using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation {
    public class CarImageAddDtoValidator : AbstractValidator<CarImageAddDto> {
        public CarImageAddDtoValidator() {
            RuleFor(c => c.CarId).NotEmpty();
            RuleFor(c => c.CarId).GreaterThan(0);
        }
    }
}

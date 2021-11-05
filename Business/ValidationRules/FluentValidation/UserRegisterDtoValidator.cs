using Core.Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation {
    public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto> {
        public UserRegisterDtoValidator() {
            RuleFor(urd => urd.FirstName).NotEmpty();
            RuleFor(urd => urd.FirstName).Length(2, 255);

            RuleFor(urd => urd.LastName).NotEmpty();
            RuleFor(urd => urd.LastName).Length(2, 255);

            RuleFor(urd => urd.Email).NotEmpty();
            RuleFor(urd => urd.Email).Length(5, 255);
            RuleFor(urd => urd.Email).EmailAddress();

            RuleFor(urd => urd.Password).NotEmpty();
            RuleFor(urd => urd.Password).Length(10, 255);
        }
    }
}

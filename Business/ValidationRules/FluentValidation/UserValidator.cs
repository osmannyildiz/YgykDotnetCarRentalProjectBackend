using Business.Constants;
using Core.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.ValidationRules.FluentValidation {
    public class UserValidator : AbstractValidator<User> {
        public UserValidator() {
            RuleFor(u => u.FirstName).NotEmpty();
            RuleFor(u => u.FirstName).Length(2, 255);

            RuleFor(u => u.LastName).NotEmpty();
            RuleFor(u => u.LastName).Length(2, 255);

            RuleFor(u => u.Email).NotEmpty();
            RuleFor(u => u.Email).Length(5, 255);
            RuleFor(u => u.Email).EmailAddress();
        }
    }
}

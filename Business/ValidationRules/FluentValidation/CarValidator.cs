using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation {
    public class CarValidator : AbstractValidator<Car> {
        public CarValidator() {
            RuleFor(c => c.BrandId).NotEmpty();
            RuleFor(c => c.BrandId).GreaterThan(0);

            RuleFor(c => c.ColorId).NotEmpty();
            RuleFor(c => c.ColorId).GreaterThan(0);

            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Name).Length(2, 255);

            RuleFor(c => c.ModelYear).NotEmpty();
            RuleFor(c => c.ModelYear).InclusiveBetween(1800, DateTime.Now.Year);

            RuleFor(c => c.DailyPrice).NotEmpty();
            RuleFor(c => c.DailyPrice).InclusiveBetween(100, 1000000);

            RuleFor(c => c.MinimumFindexScore).NotEmpty();
            RuleFor(c => c.MinimumFindexScore).InclusiveBetween(1, 1900);
        }
    }
}

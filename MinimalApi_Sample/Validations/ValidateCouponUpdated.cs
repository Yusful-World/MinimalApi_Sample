using FluentValidation;
using MinimalApi_Sample.Dtos;

namespace MinimalApi_Sample.Validations
{
    public class ValidateCouponUpdated : AbstractValidator<UpdateCouponDto>
    {
        public ValidateCouponUpdated()
        {
            RuleFor(model => model.Id).NotEmpty().GreaterThan(0);
            RuleFor(model => model.Name).NotEmpty();
            RuleFor(model => model.PercentDiscount).InclusiveBetween(1, 100);
        }
    }
}

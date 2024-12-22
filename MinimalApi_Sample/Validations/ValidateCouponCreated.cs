using FluentValidation;
using MinimalApi_Sample.Dtos;

namespace MinimalApi_Sample.Validations
{
    public class ValidateCouponCreated : AbstractValidator<CreateCouponDto>
    {
        public ValidateCouponCreated()
        {
            RuleFor(model => model.Name).NotEmpty();
            RuleFor(model => model.PercentDiscount).InclusiveBetween(1, 100);
        }
    }
}

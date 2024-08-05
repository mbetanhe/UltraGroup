using FluentValidation;
using UltraGroup.Core.Application.Filters;

namespace UltraGroup.Core.Application.Validators
{
    public class HotelFilterValidator : AbstractValidator<HotelFilter>
    {
        public HotelFilterValidator()
        {
            RuleFor(e => e.startDate).DateStringFormat("yyyy-MM-dd");
            RuleFor(e => e.EndDate).DateStringFormat("yyyy-MM-dd");
            RuleFor(e => e.LocationId).Must(x => x > 0).WithMessage("LocationId  must be greaer than zero");
            RuleFor(e => e.Quantity).Must(x => x > 0).WithMessage("Must contain at least one client");
        }
    }
}

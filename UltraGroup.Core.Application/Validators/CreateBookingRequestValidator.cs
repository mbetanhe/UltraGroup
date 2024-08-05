using FluentValidation;
using UltraGroup.Core.Application.Requests.Bookings;

namespace UltraGroup.Core.Application.Validators
{
    public class CreateBookingRequestValidator : AbstractValidator<CreateBookingRequest>
    {
        public CreateBookingRequestValidator()
        {
            RuleFor(e => e.RoomId).NotEmpty().WithMessage("Room Id cannot be empty");
            RuleFor(e => e.PhoneEmergency).NotEmpty().NotNull().WithMessage("The emergency phone cannot be empty");
            RuleFor(e => e.PhoneEmergency).MaximumLength(10).WithMessage("The maximum number of characters must be 10");
            RuleFor(e => e.Stardate).DateStringFormat("yyyy-MM-dd");
            RuleFor(e => e.EndDate).DateStringFormat("yyyy-MM-dd");
            RuleFor(e => e.clients).Must(x => x.Count > 0).WithMessage("Must contain at least one client");
        }
    }
}

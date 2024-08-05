using FluentValidation;
using UltraGroup.Core.Application.Requests.Rooms;

namespace UltraGroup.Core.Application.Validators
{
    public class CreateUpdateHotelRequestValidators : AbstractValidator<CreateUpdateRoomRequest>
    {
        public CreateUpdateHotelRequestValidators()
        {
            RuleFor(e => e.Description).NotEmpty().WithMessage("Description canno be empty");
            RuleFor(e => e.Price).Must(x => x > 0).WithMessage("Price must be greaer than zero");
            RuleFor(e => e.Tax).Must(x => x > 0).WithMessage("Tax must be greaer than zero");
            RuleFor(e => e.IdType).Must(x => x > 0).WithMessage("IdType  must be greaer than zero");
        }
    }
}

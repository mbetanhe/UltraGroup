using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraGroup.Core.Application.Requests.Hotel;

namespace UltraGroup.Core.Application.Validators
{
    public class CreateUpdateRoomRequestValidator : AbstractValidator<CreateUpdateHotelRequest>
    {
        public CreateUpdateRoomRequestValidator()
        {
                
        }
    }
}

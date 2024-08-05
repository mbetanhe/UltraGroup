using AutoMapper;
using FluentValidation;
using MediatR;
using UltraGroup.Core.Application.CQRS.Commands;
using UltraGroup.Core.Application.CQRS.Queries;
using UltraGroup.Core.Application.Interfaces;
using UltraGroup.Core.Application.Requests.Bookings;
using UltraGroup.Core.Application.Requests.Hotel;
using UltraGroup.Core.Application.Responses;
using UltraGroup.Core.Application.Responses.Hotel;
using UltraGroup.Core.Domain.Entities;

namespace UltraGroup.Infrastructure.Services
{
    public class HotelService : IHotelService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IRoomService _roomService;
        private readonly IValidator<CreateUpdateHotelRequest> _validator;

        public HotelService(IMapper mapper, IMediator mediator, IRoomService roomService, IValidator<CreateUpdateHotelRequest> validator)
        {
            _mapper = mapper;
            _mediator = mediator;
            _roomService = roomService;
            _validator = validator; 
        }

        public async Task<IResult> GetAsync(int id)
        {
            if (id == 0)
                return Result.Fail("The identifier couldn't be zero");

            var result = await _mediator.Send(new GetHotelQuery() { HotelId = id });

            return Result<GetHotelResponse>.Success(_mapper.Map<GetHotelResponse>(result.Data));
        }

        public async Task<IResult> CreateAsync(CreateUpdateHotelRequest data)
        {
            if (data == null)
                return Result.Fail("Data from hotel cannot be null");

            if (data.Id != 0)
            {
                var hotel = await GetAsync(data.Id);

                if (hotel != null)
                    return Result.Fail("Hotel already exists");
            }

            var validationResult = await _validator.ValidateAsync(data);
            if (!validationResult.IsValid)
            {
                return Result.Fail(erorrs: validationResult.ToDictionary());
            }

            var entity = _mapper.Map<HotelEntity>(data);

            var result = await _mediator.Send(new CreateHotelCommand() { data = entity, UserId = data.UserID });

            if (!result.Succeeded)
            {
                return Result.Fail(result.Messages[0]);
            }

            return Result<CreateUpdateHotelResponse>.Success(data: _mapper.Map<CreateUpdateHotelResponse>(result.Data), "The hotel has been registered successfully");
        }

        public async Task<IResult> SetRoomAsync(SetRoomRequest data)
        {
            if (data == null)
                return Result.Fail("Data from hotel and room cannot be null");

            if (data.HotelId == 0)
                return Result.Fail("Data from hotel cannot be zero");

            if (data.RoomId == 0)
                return Result.Fail("Data from room cannot be zero");

            var result = await _roomService.GetAsync(data.RoomId);

            if (result == null)
                return Result.Fail("The room does not exits");


            if (result.Succeeded)
            {
                if(result.Data.Identifier != 0)
                    return Result.Fail("The room already asigned to other hotel");
            }

            var res = await _mediator.Send(new SetRoomsHotelCommand() { data = data });

            if (!res.Succeeded)
                return Result.Fail(res.Messages[0]);

            return Result<bool>.Success(res.Data, res.Messages);

        }

        public async Task<IResult> UpdateAsync(CreateUpdateHotelRequest data)
        {
            if (data == null)
                return Result.Fail("Data from hotel cannot be null");

            if (data.Id == 0)
                return Result.Fail("Hotel identifier cannot be zero");

            var hotel = await GetAsync(data.Id);

            if (hotel == null)
                return Result.Fail("Hotel for updating does not exists");

            var validationResult = await _validator.ValidateAsync(data);
            if (!validationResult.IsValid)
            {
                return Result.Fail(erorrs: validationResult.ToDictionary());
            }

            var entity = _mapper.Map<HotelEntity>(data);

            var result = await _mediator.Send(new UpdateHotelCommand() { data = entity });

            if (!result.Succeeded)
            {
                return Result.Fail(result.Messages[0]);
            }

            return Result<CreateUpdateHotelResponse>.Success(data: _mapper.Map<CreateUpdateHotelResponse>(result.Data), "The hotel has been updated successfully");
        }

        public async Task<IResult> EnableOrDisableAsync(EnableDisableHotelRequest data)
        {
            if (data == null)
                return Result.Fail("Data cannot be null");

            if (data.HotelId == 0)
                return Result.Fail("Hotel identifier cannot be zero");

            var hotel = await GetAsync(data.HotelId);
            if (hotel == null)
                return Result.Fail("Hotel for updating does not exists");

            var result = await _mediator.Send(new EnableDisableHotelCommand() { data = data });

            if (!result.Succeeded)
                return Result.Fail(result.Messages);

            return Result<bool>.Success(result.Data, result.Messages[0]);

        }
    }
}

using AutoMapper;
using FluentValidation;
using MediatR;
using UltraGroup.Core.Application.CQRS.Commands;
using UltraGroup.Core.Application.CQRS.Queries;
using UltraGroup.Core.Application.Filters;
using UltraGroup.Core.Application.Interfaces;
using UltraGroup.Core.Application.Requests.Rooms;
using UltraGroup.Core.Application.Responses;
using UltraGroup.Core.Application.Responses.Room;
using UltraGroup.Core.Domain.Entities;

namespace UltraGroup.Infrastructure.Services
{
    public class RoomService : IRoomService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IValidator<CreateUpdateRoomRequest> _validator;
        private readonly IValidator<HotelFilter> _validatorFilter;

        public RoomService(IMapper mapper, IMediator mediator, IValidator<CreateUpdateRoomRequest> validator, IValidator<HotelFilter> validatorFilter)
        {
            _mapper = mapper;
            _mediator = mediator;
            _validator = validator;
            _validatorFilter = validatorFilter;
        }

        public async Task<IResult<GetRoomResponse>> GetAsync(int id)
        {
            if (id == 0)
                return Result<GetRoomResponse>.Fail("The identifier couldn't be zero");

            var result = await _mediator.Send(new GetRoomQuery() { roomId = id });

            return Result<GetRoomResponse>.Success(_mapper.Map<GetRoomResponse>(result.Data));
        }

        public async Task<IResult<List<GetRoomResponse>>> GetListAsync()
        {
            var result = await _mediator.Send(new GetRoomListQuery());

            return Result<List<GetRoomResponse>>.Success(_mapper.Map<List<GetRoomResponse>>(result.Data));
        }

        public async Task<IResult<List<GetRoomResponse>>> GetReservedOrNotAsync(bool data)
        {
            var result = await _mediator.Send(new GetReservedOrNotRoomsQuery() { IsReserved = data });

            return Result<List<GetRoomResponse>>.Success(_mapper.Map<List<GetRoomResponse>>(result.Data));
        }

        public async Task<IResult> CreateAsync(CreateUpdateRoomRequest data)
        {
            if (data == null)
                return Result.Fail("Data from room cannot be null");

            var validationResult = await _validator.ValidateAsync(data);
            if (!validationResult.IsValid)
            {
                return Result.Fail(erorrs: validationResult.ToDictionary());
            }

            var entity = _mapper.Map<RoomEntity>(data);

            var result = await _mediator.Send(new CreateRoomCommand() { data = entity });

            if (!result.Succeeded)
            {
                return Result.Fail(result.Messages[0]);
            }

            return Result<CreateUpdateRoomResponse>.Success(data: _mapper.Map<CreateUpdateRoomResponse>(result.Data), "The room has been registered successfully");
        }

        public async Task<IResult> UpdateAsync(CreateUpdateRoomRequest data)
        {
            if (data == null)
                return Result.Fail("Data from room cannot be null");

            if (data.Id == 0)
                return Result.Fail("Room identifier cannot be zero");

            var room = await GetAsync(data.Id);

            if (room == null)
                return Result.Fail("Room for updating does not exists");

            var validationResult = await _validator.ValidateAsync(data);
            if (!validationResult.IsValid)
            {
                return Result.Fail(erorrs: validationResult.ToDictionary());
            }

            var entity = _mapper.Map<RoomEntity>(data);

            var result = await _mediator.Send(new UpdateRoomCommand() { data = entity });

            if (!result.Succeeded)
            {
                return Result.Fail(result.Messages[0]);
            }

            return Result<CreateUpdateRoomResponse>.Success(data: _mapper.Map<CreateUpdateRoomResponse>(result.Data), "The room has been updated successfully");
        }

        public async Task<IResult> EnableOrDisableAsync(EnableDisableRoomRequest data)
        {
            if (data == null)
                return Result.Fail("Data cannot be null");

            if (data.RoomId == 0)
                return Result.Fail("Room identifier cannot be zero");

            var hotel = await GetAsync(data.RoomId);
            if (hotel == null)
                return Result.Fail("Room for updating does not exists");

            var result = await _mediator.Send(new EnableDisableRoomCommand() { data = data });

            if (!result.Succeeded)
                return Result.Fail(result.Messages);

            return Result<bool>.Success(result.Data, result.Messages[0]);

        }

        public async Task<IResult> SetReveationtoRoomAsync(int id)
        {
            if (id == 0)
                return Result.Fail("Data from room cannot be zero");

            var room = await GetAsync(id);
            if (room == null)
                return Result.Fail("Room does not exists");

            var result = await _mediator.Send(new SetReservationRoomCommand() { RoomId = id });

            if (!result.Succeeded)
                return Result.Fail(result.Messages);

            return Result<bool>.Success(result.Data, "Rservation is completed");
        }

        public async Task<IResult> GetAvailableRooms(HotelFilter data)
        {
            var validationResult = await _validatorFilter.ValidateAsync(data);
            if (!validationResult.IsValid)
            {
                return Result.Fail(erorrs: validationResult.ToDictionary());
            }

            var result = await _mediator.Send(new GetAvailableRoomsQuery() { Filter = data });
            if (!result.Succeeded)
                return Result.Fail(result.Messages);

            return Result<List<GetRoomResponse>>.Success(_mapper.Map<List<GetRoomResponse>>(result.Data), result.Messages);
        }
    }
}

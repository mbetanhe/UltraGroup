using AutoMapper;
using FluentValidation;
using MediatR;
using UltraGroup.Core.Application.CQRS.Commands;
using UltraGroup.Core.Application.CQRS.Queries;
using UltraGroup.Core.Application.Interfaces;
using UltraGroup.Core.Application.Requests.Bookings;
using UltraGroup.Core.Application.Responses;
using UltraGroup.Core.Application.Responses.Booking;
using UltraGroup.Core.Application.Responses.Hotel;
using UltraGroup.Core.Application.Responses.Room;
using UltraGroup.Core.Domain.Entities;

namespace UltraGroup.Infrastructure.Services
{
    public class BookingService : IBookingService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IRoomService _roomService;
        private readonly IValidator<CreateBookingRequest> _validator;
        private readonly IEmailService _emailService;   

        public BookingService(IMapper mapper, IMediator mediator, IRoomService roomService, IValidator<CreateBookingRequest> validator, IEmailService emailService)
        {
            _mapper = mapper;
            _mediator = mediator;
            _roomService = roomService;
            _validator = validator;
            _emailService = emailService;
        }

        public async Task<IResult> CreateAsync(CreateBookingRequest data)
        {
            if (data == null)
                return Result.Fail("Data from reservation cannot be null");

            var validationResult = await _validator.ValidateAsync(data);
            if (!validationResult.IsValid)
            {
                return Result.Fail(erorrs: validationResult.ToDictionary());
            }

            GetRoomResponse? resultRoom;

            var reservedRooms = await _roomService.GetReservedOrNotAsync(true);

            if (reservedRooms != null && reservedRooms.Data.Count > 0)
            {
                resultRoom = reservedRooms.Data.FirstOrDefault(x => x.Identifier == data.RoomId);
                if (resultRoom != null)
                    return Result.Fail("The room already has a reservation");
            }

            var entity = _mapper.Map<BookingEntity>(data);

            var result = await _mediator.Send(new CreateBookingCommand() { data = entity });

            if (!result.Succeeded)
            {
                return Result.Fail(result.Messages[0]);
            }

            await _roomService.SetReveationtoRoomAsync(data.RoomId);
            //await _emailService.SendMailAsync(data.clients.FirstOrDefault().Email, "Reservation from Hotel", data.clients.FirstOrDefault().FullName);

            return Result<bool>.Success(data: _mapper.Map<bool>(result.Data), "The reservation has been registered successfully");
        }

        public async Task<IResult> GetBookingsByHotel(int data)
        {
            if (data == 0)
                return Result.Fail("The identifier couldn't be zero");

            var result = await _mediator.Send(new GetBookingsByHotelQuery() { HotelId = data });

            return Result<List<GetBookingResponse>>.Success(_mapper.Map<List<GetBookingResponse>>(result.Data));
        }

        public async Task<IResult> GetBookingsDetailsl(int data)
        {
            if (data == 0)
                return Result.Fail("The identifier couldn't be zero");

            var result = await _mediator.Send(new GetBookingQuery() { BookingId = data });

            if(!result.Succeeded)
                return Result.Fail(result.Messages); 

            return Result<GetBookingDetailResponse>.Success(_mapper.Map<GetBookingDetailResponse>(result.Data));
        }

    }
}

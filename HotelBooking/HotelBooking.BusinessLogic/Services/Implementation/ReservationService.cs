using HotelBooking.BusinessLogic.Dtos;
using HotelBooking.BusinessLogic.Exceptions;
using HotelBooking.BusinessLogic.Services.Abstraction;
using HotelBooking.DataAccessLayer.Entities;
using HotelBooking.DataAccessLayer.Repositories.Interfaces;

namespace HotelBooking.BusinessLogic.Services.Implementation;
public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IGuestRepository _guestRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidationService<CreateReservationDto> _validationService;

    public ReservationService(IReservationRepository reservationRepository, IGuestRepository guestRepository,
                                       IRoomRepository roomRepository, IUnitOfWork unitOfWork, IValidationService<CreateReservationDto> validationService)
    {
        _reservationRepository = reservationRepository;
        _guestRepository = guestRepository;
        _roomRepository = roomRepository;
        _unitOfWork = unitOfWork;
        _validationService = validationService;
    }

    public async Task<ReservationDto> GetReservation(int id)
    {
        var reservation = await _reservationRepository.GetReservation(id);
        if (reservation is null)
        {
            throw new NotFoundException($"Reservation with Id = {id} does not exists.");
        }
        var roomInfos = new List<BasicRoomInfo>();

        roomInfos = reservation.Rooms.Select(c => new BasicRoomInfo((Dtos.RoomType)c.Type, c.Capacity)).ToList();

        var dto = new ReservationDto()
        {
            Id = reservation.ReservationId,
            DateFrom = reservation.DateFrom,
            DateTo = reservation.DateTo,
            Price = reservation.Price,
            GuestFirstname = reservation.Guest.Lastname,
            GuestLastname = reservation.Guest.Firstname,
            RoomInfos = roomInfos
        };

        return dto;
    }

    public async Task AddReservation(CreateReservationDto createReservationDto)
    {
        await _validationService.Validate(createReservationDto);

        var guest = await _guestRepository.GetGuest(createReservationDto.GuestEmail);

        if (guest is null)
        {
            throw new BadRequestException("You must register before making a reservation.");
        }

        var roomInfos = createReservationDto.Rooms;

        var rooms = new List<Room>();
        decimal price = 0;

        foreach (var roomInfo in roomInfos)
        {
            var room = await _roomRepository.GetRoom((DataAccessLayer.Entities.RoomType)roomInfo.Type, roomInfo.Capacity, createReservationDto.DateFrom, createReservationDto.DateTo);
            
            if(room is null)
            {
                throw new NotFoundException("Selected rooms are not available on the specified date.");
            }
            
            price += room.PricePerNight * (createReservationDto.DateTo - createReservationDto.DateFrom).Days;
            rooms.Add(room);
            
        }
        

        

        var reservation = new Reservation()
        {
            DateFrom = createReservationDto.DateFrom,
            DateTo = createReservationDto.DateTo,
            Price = price,
            GuestId = guest.GuestId,
            Guest = guest,
            Rooms = rooms
        };

        await _reservationRepository.AddReservation(reservation);
        _unitOfWork.Commit();

    }
}

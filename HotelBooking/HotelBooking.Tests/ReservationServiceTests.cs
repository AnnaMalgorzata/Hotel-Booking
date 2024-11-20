using HotelBooking.BusinessLogic.Dtos;
using HotelBooking.BusinessLogic.Exceptions;
using HotelBooking.BusinessLogic.Services.Abstraction;
using HotelBooking.BusinessLogic.Services.Implementation;
using HotelBooking.DataAccessLayer.Entities;
using HotelBooking.DataAccessLayer.Repositories.Interfaces;
using Moq;
using Xunit;

namespace HotelBooking.Test;
public class ReservationServiceTests
{
    private readonly Mock<IReservationRepository> _reservationRepositoryMock;
    private readonly Mock<IGuestRepository> _guestRepositoryMock;
    private readonly Mock<IRoomRepository> _roomRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IValidationService<CreateReservationDto>> _reservationValidatorMock;

    public ReservationServiceTests()
    {
        _reservationRepositoryMock = new Mock<IReservationRepository>();
        _guestRepositoryMock = new Mock<IGuestRepository>();
        _roomRepositoryMock = new Mock<IRoomRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _reservationValidatorMock = new Mock<IValidationService<CreateReservationDto>>();
    }

    [Fact]
    public async Task GetReservation_WhenExist_ReturnReservationDto()
    {
        //Arrange
        var reservationId = 1;
        var dateFrom = new DateOnly(2024, 11, 15);
        var dateTo = new DateOnly(2024, 11, 17);
        var reservation = new Reservation
        {
            ReservationId = reservationId,
            DateFrom = dateFrom,
            DateTo = dateTo,
            Price = 200,
            Guest = CreateGuest(),
            Rooms = new List<Room>
            {
                CreateRoom()
            }
        };

        _reservationRepositoryMock.Setup(repo => repo.GetReservation(reservationId))
                              .ReturnsAsync(reservation);

        var reservationService = new ReservationService(_reservationRepositoryMock.Object, _guestRepositoryMock.Object, _roomRepositoryMock.Object, _unitOfWorkMock.Object, _reservationValidatorMock.Object);

        //Act
        var result = await reservationService.GetReservation(reservationId);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(reservationId, result.Id);
        Assert.Equal(dateFrom, result.DateFrom);
        Assert.Equal(dateTo, result.DateTo);
        Assert.Equal(reservation.Price, result.Price);
        Assert.Equal(reservation.Guest.Firstname, result.GuestFirstname);
        Assert.Equal(reservation.Guest.Lastname, result.GuestLastname);
    }

    [Fact]
    public async Task GetReservation_WhenReservationDoesntExist_BadRequestExceptionThrown()
    {
        //Arrange
        var reservationId = 1;
        _reservationRepositoryMock.Setup(x => x.GetReservation(It.IsAny<int>())).ReturnsAsync(() => null);

        var reservationService = new ReservationService(_reservationRepositoryMock.Object, _guestRepositoryMock.Object, _roomRepositoryMock.Object, _unitOfWorkMock.Object, _reservationValidatorMock.Object);

        //Act & Assert
        Task action() => reservationService.GetReservation(reservationId);
        var exception = await Assert.ThrowsAsync<NotFoundException>(action);
        Assert.Equal($"Reservation with Id = {reservationId} does not exists.", exception.Message);
    }

    [Fact]
    public async Task AddReservation_WhenUnregisteredGuest_BadRequestException()
    {
        //Arrange
        var reservationService = new ReservationService(_reservationRepositoryMock.Object, _guestRepositoryMock.Object, _roomRepositoryMock.Object, _unitOfWorkMock.Object, _reservationValidatorMock.Object);
        
        _guestRepositoryMock.Setup(x => x.GetGuest(It.IsAny<string>())).ReturnsAsync(() => null);

        var createReservationDto = CreateReservationDto();

        //Act & Assert
        Task action() => reservationService.AddReservation(createReservationDto);
        var exception = await Assert.ThrowsAsync<BadRequestException>(action);
        // Verify that GetGuest was called with the correct email
        _guestRepositoryMock.Verify(x => x.GetGuest(createReservationDto.GuestEmail), Times.Once);
        Assert.Equal("You must register before making a reservation.", exception.Message);
    }

    [Fact]
    public async Task AddReservation_WhenRoomsAreUnavailable_BadRequestException()
    {
        //Arrange
        var reservationService = new ReservationService(_reservationRepositoryMock.Object, _guestRepositoryMock.Object, _roomRepositoryMock.Object, _unitOfWorkMock.Object, _reservationValidatorMock.Object);
        var guest = CreateGuest();

        _guestRepositoryMock.Setup(x => x.GetGuest(It.IsAny<string>())).ReturnsAsync(guest);
        _roomRepositoryMock.Setup(x => x.GetRoom(It.IsAny<DataAccessLayer.Entities.RoomType>(), It.IsAny<int>(), It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).ReturnsAsync(() => null);

        var createReservationDto = CreateReservationDto();

        //Act & Assert
        Task action() => reservationService.AddReservation(createReservationDto);
        var exception = await Assert.ThrowsAsync<BadRequestException>(action);

        // Verify that GetGuest was called with the correct email
        _guestRepositoryMock.Verify(x => x.GetGuest(createReservationDto.GuestEmail), Times.Once);

        // Verify that GetRoom was called for each room in the request
        foreach (var roomInfo in createReservationDto.Rooms)
        {
            _roomRepositoryMock.Verify(x => x.GetRoom((DataAccessLayer.Entities.RoomType)roomInfo.Type, roomInfo.Capacity, createReservationDto.DateFrom, createReservationDto.DateTo), Times.Once);
        }

        // Check exception message
        Assert.Equal("Selected rooms are not available on the specified date.", exception.Message);
    }

    [Fact]
    public async Task AddReservation_WhenOk_ReturnReservationId()
    {
        //Arrange
        var reservationService = new ReservationService(_reservationRepositoryMock.Object, _guestRepositoryMock.Object, _roomRepositoryMock.Object, _unitOfWorkMock.Object, _reservationValidatorMock.Object);
        var guest = CreateGuest();

        _guestRepositoryMock.Setup(x => x.GetGuest(It.IsAny<string>())).ReturnsAsync(guest);
        _reservationRepositoryMock.Setup(x => x.AddReservation(It.IsAny<Reservation>()))
            .Callback<Reservation>(reservation => reservation.ReservationId = 1); // Ustawienie ReservationId na 1

        var room = CreateRoom();

        var createReservationDto = CreateReservationDto();

        _roomRepositoryMock.Setup(x => x.GetRoom(It.IsAny<DataAccessLayer.Entities.RoomType>(), It.IsAny<int>(), It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).ReturnsAsync(room);

        //Act & Assert
        var result = await reservationService.AddReservation(createReservationDto);
        Assert.Equal(1, result);
    }

    private CreateReservationDto CreateReservationDto(
        string email = null,
        DateOnly? dateFrom = null,
        DateOnly? dateTo = null,
        ICollection<BasicRoomInfo> rooms = null)
        => new CreateReservationDto
        {
            GuestEmail = email ?? "anna@gmail.com",
            DateFrom = dateFrom ?? new DateOnly(2024, 11, 15),
            DateTo = dateTo ?? new DateOnly(2024, 11, 17),
            Rooms = rooms ?? new List<BasicRoomInfo>
            {
                new BasicRoomInfo(BusinessLogic.Dtos.RoomType.Apartment, 2)
            }
        };

    private Guest CreateGuest(
        int? guestId = null,
        string firstname = null,
        string lastname = null,
        string email = null,
        string phoneNumber = null,
        DateOnly? dateBirth = null)
        => new Guest
        {
            GuestId = guestId ?? 1,
            Firstname = firstname ?? "Anna",
            Lastname = lastname ?? "Xyz",
            Email = email ?? "anna@gmail.com",
            PhoneNumber = phoneNumber ?? "123456789",
            DateBirth = dateBirth ?? new DateOnly(2000, 9, 28),
        };

    private Room CreateRoom(
        int? roomId = null,
        int? number = null,
        int? floor = null,
        DataAccessLayer.Entities.RoomType? type = null,
        int? capacity = null,
        decimal? pricePerNight = null)
        => new Room
        {
            RoomId = roomId ?? 1,
            Number = number ?? 1,
            Floor = floor ?? 1,
            Type = type ?? DataAccessLayer.Entities.RoomType.Apartment,
            Capacity = capacity ?? 2,
            PricePerNight = pricePerNight ?? 249,
        };
}

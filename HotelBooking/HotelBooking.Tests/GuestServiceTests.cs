using HotelBooking.BusinessLogic.Dtos;
using HotelBooking.BusinessLogic.Exceptions;
using HotelBooking.BusinessLogic.Services.Abstraction;
using HotelBooking.BusinessLogic.Services.Implementation;
using HotelBooking.DataAccessLayer.Entities;
using HotelBooking.DataAccessLayer.Repositories.Interfaces;
using Moq;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace HotelBooking.Tests;
public class GuestServiceTests
{
    private readonly Mock<IGuestRepository> _guestRepository;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IValidationService<GuestDto>> _validationService;

    public GuestServiceTests()
    {
        _guestRepository = new Mock<IGuestRepository>();
        _unitOfWork = new Mock<IUnitOfWork>();
        _validationService = new Mock<IValidationService<GuestDto>>();
    }

    [Fact]
    public async Task AddGuest_WhenNewEmail()
    {
        //Arrange
        var guestService = new GuestService(_guestRepository.Object, _unitOfWork.Object, _validationService.Object);
        _guestRepository.Setup(x => x.GetGuest(It.IsAny<string>())).ReturnsAsync(() => null);

        var guest = CreateGuestDto();

        //Act
        await guestService.AddGuest(guest);

        //Assert
        _unitOfWork.Verify(x => x.Commit(), Times.Once);
    }

    [Fact]
    public async Task AddGuest_WhenEmailAlreadyExists_BadRequestException()
    {
        //Arrange
        var guestService = new GuestService(_guestRepository.Object, _unitOfWork.Object, _validationService.Object);

        var guestDto = CreateGuestDto();
        var hmac = new HMACSHA256();

        var guest = new Guest()
        {
            GuestId = 1,
            Firstname = guestDto.Firstname,
            Lastname = guestDto.Lastname,
            Email = guestDto.Email,
            PhoneNumber = guestDto.PhoneNumber,
            DateBirth = guestDto.DateBirth,
            PasswordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes("SomePassword")))
        };

        _guestRepository.Setup(x => x.GetGuest(It.IsAny<string>())).ReturnsAsync(guest);

        //Act & Assert
        Task action() => guestService.AddGuest(guestDto);
        var exception = await Assert.ThrowsAsync<BadRequestException>(action);
        _guestRepository.Verify(x => x.GetGuest(guest.Email), Times.Once);
        Assert.Equal("A customer with this email already exists.", exception.Message);
    }

    private GuestDto CreateGuestDto(
        string email = null,
        string firstName = null,
        string lastName = null,
        string phone = null)
        => new GuestDto
        {
            Email = email ?? "anna@gmail.com",
            Firstname = firstName ?? "Anna",
            Lastname = lastName ?? "Xyz",
            PhoneNumber = phone ?? "123456789",
            DateBirth = new DateOnly(2000, 9, 28),
            Password = "TestPassword123"
        };
}

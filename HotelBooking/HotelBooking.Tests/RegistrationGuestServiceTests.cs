using HotelBooking.BusinessLogic.Dtos;
using HotelBooking.BusinessLogic.Exceptions;
using HotelBooking.BusinessLogic.Services.Abstraction;
using HotelBooking.BusinessLogic.Services.Implementation;
using HotelBooking.BusinessLogic.Utilities;
using HotelBooking.DataAccessLayer.Entities;
using HotelBooking.DataAccessLayer.Repositories.Interfaces;
using Moq;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace HotelBooking.Tests;
public class RegistrationServiceTests
{
    private readonly Mock<IGuestRepository> _guestRepository;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IValidationService<RegistrationDto>> _validationService;

    public RegistrationServiceTests()
    {
        _guestRepository = new Mock<IGuestRepository>();
        _unitOfWork = new Mock<IUnitOfWork>();
        _validationService = new Mock<IValidationService<RegistrationDto>>();
    }

    [Fact]
    public async Task RegisterGuest_WhenNewEmail()
    {
        //Arrange
        var guestService = new RegistrationService(_guestRepository.Object, _unitOfWork.Object, _validationService.Object);
        _guestRepository.Setup(x => x.GetGuest(It.IsAny<string>())).ReturnsAsync(() => null);

        var guest = CreateGuestDto();

        //Act
        await guestService.RegisterGuest(guest);

        //Assert
        _unitOfWork.Verify(x => x.Commit(), Times.Once);
    }

    [Fact]
    public async Task RegisterGuest_WhenEmailAlreadyExists_BadRequestException()
    {
        //Arrange
        var guestService = new RegistrationService(_guestRepository.Object, _unitOfWork.Object, _validationService.Object);

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
            PasswordHash = PasswordHasher.HashPassword("SomePassword")
        };

        _guestRepository.Setup(x => x.GetGuest(It.IsAny<string>())).ReturnsAsync(guest);

        //Act & Assert
        Task action() => guestService.RegisterGuest(guestDto);
        var exception = await Assert.ThrowsAsync<BadRequestException>(action);
        _guestRepository.Verify(x => x.GetGuest(guest.Email), Times.Once);
        Assert.Equal("A customer with this email already exists.", exception.Message);
    }

    [Fact]
    public async Task AddGuest_WhenPasswordsDoNotMatch_BadRequestException()
    {
        // Arrange
        var guestService = new RegistrationService(_guestRepository.Object, _unitOfWork.Object, _validationService.Object);

        var guestDto = CreateGuestDto(password: "TestPassword123", passwordConfirmation: "DifferentPassword456");

        // Act & Assert
        Task action() => guestService.RegisterGuest(guestDto);
        var exception = await Assert.ThrowsAsync<BadRequestException>(action);

        Assert.Equal("Passwords do not match.", exception.Message);
    }


    private RegistrationDto CreateGuestDto(
        string email = null,
        string firstName = null,
        string lastName = null,
        string phone = null,
        string password = null,
        string passwordConfirmation = null)
        => new RegistrationDto
        {
            Email = email ?? "anna@gmail.com",
            Firstname = firstName ?? "Anna",
            Lastname = lastName ?? "Xyz",
            PhoneNumber = phone ?? "123456789",
            DateBirth = new DateOnly(2000, 9, 28),
            Password = password ?? "TestPassword123",
            PasswordConfirmation = passwordConfirmation ?? "TestPassword123"
        };
}

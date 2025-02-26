using HotelBooking.BusinessLogic.Dtos;
using HotelBooking.BusinessLogic.Exceptions;
using HotelBooking.BusinessLogic.Services.Abstraction;
using HotelBooking.BusinessLogic.Utilities;
using HotelBooking.DataAccessLayer.Entities;
using HotelBooking.DataAccessLayer.Repositories.Interfaces;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("HotelBooking.Tests")]
namespace HotelBooking.BusinessLogic.Services.Implementation;
internal class RegistrationService : IRegistrationService
{
    private readonly IGuestRepository _guestRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidationService<RegistrationDto> _validationService;

    public RegistrationService(IGuestRepository guestRepository, IUnitOfWork unitOfWork, IValidationService<RegistrationDto> validationService)
    {
        _guestRepository = guestRepository;
        _unitOfWork = unitOfWork;
        _validationService = validationService;
    }

    public async Task RegisterGuest(RegistrationDto guestDto)
    {
        await _validationService.Validate(guestDto);

        if(await _guestRepository.GetGuest(guestDto.Email) != null)
        {
            throw new BadRequestException("A customer with this email already exists.");
        }
        if (!string.Equals(guestDto.Password, guestDto.PasswordConfirmation, StringComparison.Ordinal))
        {
            throw new BadRequestException("Passwords do not match.");
        }
        
        var guest = new Guest()
        {
            Firstname = guestDto.Firstname,
            Lastname = guestDto.Lastname,
            Email = guestDto.Email,
            PhoneNumber = guestDto.PhoneNumber,
            DateBirth = guestDto.DateBirth,
            PasswordHash = PasswordHasher.HashPassword(guestDto.Password),
        };
        _guestRepository.Add(guest);
        _unitOfWork.Commit();
        
    }

    
}


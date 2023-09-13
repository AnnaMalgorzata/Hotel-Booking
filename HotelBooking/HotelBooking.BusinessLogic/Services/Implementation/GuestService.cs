using FluentValidation.Results;
using HotelBooking.BusinessLogic.Dtos;
using HotelBooking.BusinessLogic.Exceptions;
using HotelBooking.BusinessLogic.Services.Abstraction;
using HotelBooking.BusinessLogic.Validators;
using HotelBooking.DataAccessLayer.Entities;
using HotelBooking.DataAccessLayer.Repositories.Interfaces;
using System.Text;

namespace HotelBooking.BusinessLogic.Services.Implementation;
public class GuestService : IGuestService
{
    private readonly IGuestRepository _guestRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GuestService(IGuestRepository guestRepository, IUnitOfWork unitOfWork)
    {
        _guestRepository = guestRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task AddGuest(GuestDto guestDto)
    {
        var validator = new GuestValidator();
        ValidationResult result = await validator.ValidateAsync(guestDto);
        var message = new StringBuilder();

        if (!result.IsValid)
        {
            message.Append("Invalid fields: ");

            foreach (var failure in result.Errors)
            {
                message.Append(failure.ErrorMessage + ", ");
            }

            throw new BadRequestException(message.Remove(message.Length - 2, 2).ToString());

        }
        else
        {
            var guest = new Guest()
            {
                Firstname = guestDto.Firstname,
                Lastname = guestDto.Lastname,
                Email = guestDto.Email,
                PhoneNumber = guestDto.PhoneNumber,
                DateBirth = guestDto.DateBirth,
            };

            _guestRepository.Add(guest);
            _unitOfWork.Commit();
        }

    }
}


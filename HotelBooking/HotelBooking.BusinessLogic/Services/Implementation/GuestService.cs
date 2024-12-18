﻿using HotelBooking.BusinessLogic.Dtos;
using HotelBooking.BusinessLogic.Exceptions;
using HotelBooking.BusinessLogic.Services.Abstraction;
using HotelBooking.DataAccessLayer.Entities;
using HotelBooking.DataAccessLayer.Repositories.Interfaces;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("HotelBooking.Tests")]
namespace HotelBooking.BusinessLogic.Services.Implementation;
internal class GuestService : IGuestService
{
    private readonly IGuestRepository _guestRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidationService<GuestDto> _validationService;

    public GuestService(IGuestRepository guestRepository, IUnitOfWork unitOfWork, IValidationService<GuestDto> validationService)
    {
        _guestRepository = guestRepository;
        _unitOfWork = unitOfWork;
        _validationService = validationService;
    }

    public async Task AddGuest(GuestDto guestDto)
    {
        await _validationService.Validate(guestDto);

        if(await _guestRepository.GetGuest(guestDto.Email) != null)
        {
            throw new BadRequestException("A customer with this email already exists.");
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


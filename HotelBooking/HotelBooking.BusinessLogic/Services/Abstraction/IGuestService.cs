using HotelBooking.BusinessLogic.Dtos;

namespace HotelBooking.BusinessLogic.Services.Abstraction;
public interface IGuestService
{
    public Task AddGuest(GuestDto guestDto);
}

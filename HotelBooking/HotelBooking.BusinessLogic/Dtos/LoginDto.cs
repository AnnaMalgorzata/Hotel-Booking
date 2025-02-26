namespace HotelBooking.BusinessLogic.Dtos;
public sealed record LoginDto
{
    public string Email { get; init; }
    public string Password { get; init; }
}

namespace HotelBooking.BusinessLogic.Services.Abstraction;
internal interface IJWTService
{
    string GenerateJWT(string email);
}

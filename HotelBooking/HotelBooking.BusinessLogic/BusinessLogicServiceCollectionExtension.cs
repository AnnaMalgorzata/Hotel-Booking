using HotelBooking.BusinessLogic.Services.Abstraction;
using HotelBooking.BusinessLogic.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace HotelBooking.BusinessLogic;
public static class BusinessLogicServiceCollectionExtension
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
    {
        services.AddScoped<IReservationService, ReservationService>();

        return services;
    }
}

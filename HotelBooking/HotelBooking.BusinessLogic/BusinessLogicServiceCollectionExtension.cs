using HotelBooking.BusinessLogic.Services.Abstraction;
using HotelBooking.BusinessLogic.Services.Implementation;
using HotelBooking.DataAccessLayer.Database;
using HotelBooking.DataAccessLayer.Repositories.Interfaces;
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

using HotelBooking.API.Middlewares;
using HotelBooking.BusinessLogic.Services.Abstraction;
using HotelBooking.BusinessLogic.Services.Implementation;

namespace HotelBooking.API;

public static class ApiCollectionExtension
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddTransient<ExceptionsMiddleware>();
        services.AddScoped<IGuestService, GuestService>();

        return services;
    }
}

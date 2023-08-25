using HotelBooking.API.Middlewares;

namespace HotelBooking.API;

public static class ApiCollectionExtension
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddTransient<ExceptionsMiddleware>();

        return services;
    }
}

using HotelBooking.API.Middlewares;
using Microsoft.OpenApi.Models;

namespace HotelBooking.API;

public static class ApiCollectionExtension
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddTransient<ExceptionsMiddleware>();

        services.AddSwaggerGen(option =>
        {
            option.MapType<DateOnly>(() => new OpenApiSchema
            {
                Type = "string",
                Format = "date"
            });
        });

        return services;
    }
}

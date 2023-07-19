using HotelBooking.DataAccessLayer.Database;
using Microsoft.Extensions.DependencyInjection;

namespace HotelBooking.DataAccessLayer;

public static class DataAccessLayerServiceCollectionExtension
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
    {
        services.AddDbContext<HotelContext>();

        return services;
    }
}

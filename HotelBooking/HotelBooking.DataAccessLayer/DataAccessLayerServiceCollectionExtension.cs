using HotelBooking.DataAccessLayer.Database;
using HotelBooking.DataAccessLayer.Repositories;
using HotelBooking.DataAccessLayer.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HotelBooking.DataAccessLayer;

public static class DataAccessLayerServiceCollectionExtension
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
    {
        services.AddDbContext<HotelContext>();

        services.AddScoped<IGuestRepository, GuestRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();
        services.AddScoped<IRoomRepository, RoomRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}

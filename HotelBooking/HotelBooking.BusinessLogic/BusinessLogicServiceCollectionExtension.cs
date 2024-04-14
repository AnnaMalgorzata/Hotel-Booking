using FluentValidation;
using HotelBooking.BusinessLogic.Dtos;
using HotelBooking.BusinessLogic.Services.Abstraction;
using HotelBooking.BusinessLogic.Services.Implementation;
using HotelBooking.BusinessLogic.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace HotelBooking.BusinessLogic;
public static class BusinessLogicServiceCollectionExtension
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
    {
        services.AddScoped<IReservationService, ReservationService>();
        services.AddScoped<IGuestService, GuestService>();
        services.AddScoped(typeof(IValidationService<>), typeof(ValidationService<>));
        services.AddScoped<IValidator<GuestDto>, GuestValidator>();
        services.AddScoped<IValidator<CreateReservationDto>, ReservationValidator>();

        return services;
    }
}

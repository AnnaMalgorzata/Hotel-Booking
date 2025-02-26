using FluentValidation;
using HotelBooking.BusinessLogic.Dtos;
using HotelBooking.BusinessLogic.Services.Abstraction;
using HotelBooking.BusinessLogic.Services.Implementation;
using HotelBooking.BusinessLogic.Validators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelBooking.BusinessLogic;
public static class BusinessLogicServiceCollectionExtension
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IReservationService, ReservationService>();
        services.AddScoped<IRegistrationService, RegistrationService>();
        services.AddScoped(typeof(IValidationService<>), typeof(ValidationService<>));
        services.AddScoped<IValidator<RegistrationDto>, RegistrationValidator>();
        services.AddScoped<IValidator<CreateReservationDto>, ReservationValidator>();
        services.AddScoped<IAuthService, AuthService>();

        services.Configure<AuthenticationSettings>(configuration.GetSection("Authentication"));

        return services;
    }
}

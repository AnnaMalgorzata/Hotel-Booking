using FluentValidation;
using HotelBooking.API;
using HotelBooking.API.Middlewares;
using HotelBooking.BusinessLogic;
using HotelBooking.BusinessLogic.Dtos;
using HotelBooking.BusinessLogic.Validators;
using HotelBooking.DataAccessLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDataAccessLayer();
builder.Services.AddBusinessLogicLayer();
builder.Services.AddApi();

builder.Services.AddScoped<IValidator<GuestDto>, GuestValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionsMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();

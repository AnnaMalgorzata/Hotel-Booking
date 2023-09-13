using HotelBooking.BusinessLogic.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace HotelBooking.API.Middlewares;

public class ExceptionsMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleException(ex, context);
        }
    }

    private async Task HandleException(Exception ex, HttpContext context)
    {
        var responseModel = ex switch
        {
            NotFoundException => new ResponseModel(HttpStatusCode.NotFound, ex.Message),
            BadRequestException => new ResponseModel(HttpStatusCode.BadRequest, ex.Message),
            _ => new ResponseModel(HttpStatusCode.InternalServerError, ex.Message)
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)responseModel.StatusCode;

        await context.Response.WriteAsync(JsonConvert.SerializeObject(responseModel));
    }
}

internal class ResponseModel
{
    public HttpStatusCode StatusCode { get; }
    public string Message { get; }

    public ResponseModel(HttpStatusCode statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }
}

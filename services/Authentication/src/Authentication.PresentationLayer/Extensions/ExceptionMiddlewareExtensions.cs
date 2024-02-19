using System.Net;
using Authentication.DataAccessLayer.Entities.ErrorModel;
using Authentication.DataAccessLayer.Entities.Exceptions.BadRequest;
using Authentication.DataAccessLayer.Entities.Exceptions.NotFound;
using Authentication.DataAccessLayer.Entities.Exceptions.Unauthorized;
using Common;
using Microsoft.AspNetCore.Diagnostics;

namespace Authentication.PresentationLayer.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = Constants.ApplicationJson;

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    context.Response.StatusCode = contextFeature.Error switch
                    {
                        NotFoundException => StatusCodes.Status404NotFound,
                        BadRequestException => StatusCodes.Status400BadRequest,
                        UnauthorizedException => StatusCodes.Status401Unauthorized,
                        _ => StatusCodes.Status500InternalServerError
                    };

                    await context.Response.WriteAsync(
                        new ErrorDetails
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message
                        }.ToString()
                    );
                }
            });
        });
    }
}
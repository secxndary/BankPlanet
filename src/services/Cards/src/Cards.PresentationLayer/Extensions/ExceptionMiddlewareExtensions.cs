using System.ComponentModel.DataAnnotations;
using System.Net;
using Cards.BusinessLogicLayer.Entities.Exceptions.BadRequest;
using Cards.BusinessLogicLayer.Entities.Exceptions.NotFound;
using Cards.BusinessLogicLayer.Entities.Exceptions.Unauthorized;
using Cards.DataAccessLayer.Entities.ErrorModel;
using Common;
using Common.Constants;
using Microsoft.AspNetCore.Diagnostics;

namespace Cards.PresentationLayer.Extensions;

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

                if (contextFeature is not null)
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
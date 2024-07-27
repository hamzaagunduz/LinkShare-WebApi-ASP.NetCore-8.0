using Link.Application.Common;
using Link.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.ExceptionsHandlers
{
    public sealed class NotFoundExceptionHandler(ILogger<NotFoundExceptionHandler> logger) : IExceptionHandler
    {


        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {

            if (exception is not NotFoundException)
            {
                return false;
            }

            logger.LogError(exception, "An unexpected error occurred");

            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            var model = new AppResponse(false, exception.Message, (int)HttpStatusCode.NotFound);


            await httpContext.Response.WriteAsJsonAsync(model, cancellationToken);
            return true;

        }


    }
}

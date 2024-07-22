//using Link.Application.Common;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Text.Json;
//using System.Threading.Tasks;

//namespace Link.Application.Middleware
//{
//    public interface IExceptionHandler
//    {
//        Task HandleAsync(HttpContext context, Exception exception);
//    }

//    public class CustomExceptionHandler : IExceptionHandler
//    {
//        private readonly ILogger<CustomExceptionHandler> _logger;

//        public CustomExceptionHandler(ILogger<CustomExceptionHandler> logger)
//        {
//            _logger = logger;
//        }

//        public async Task HandleAsync(HttpContext context, Exception exception)
//        {
//            _logger.LogError($"Something went wrong: {exception}");

//            context.Response.ContentType = "application/json";

//            AppResponse response;

//            switch (exception)
//            {
//                case Link.Application.Exceptions.NotFoundException ex:
//                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
//                    response = new ErrorResponse(ex.Message);
//                    break;
//                case Link.Application.Exceptions.BadRequestException ex:
//                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
//                    response = new ErrorResponse(ex.Message);
//                    break;
//                case Link.Application.Exceptions.AuthorizationException ex:
//                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
//                    response = new ErrorResponse(ex.Message);
//                    break;
//                case NullReferenceException ex:
//                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
//                    response = new ErrorResponse("Gerekli bir nesne boştu(BadRequest) Lütfen girişinizi kontrol edip tekrar deneyin.");
//                    break;
//                default:
//                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
//                    response = new ErrorResponse("Internal Server Error. Beklenmedik bir hata oluştu.");
//                    break;
//            }

//            var jsonResponse = JsonSerializer.Serialize(response);
//            await context.Response.WriteAsync(jsonResponse);
//        }
//    }
//}

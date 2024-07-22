//using Microsoft.AspNetCore.Http;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Link.Application.Middleware
//{
//    public class ExceptionMiddleware
//    {
//        private readonly RequestDelegate _next;
//        private readonly IExceptionHandler _exceptionHandler;

//        public ExceptionMiddleware(RequestDelegate next, IExceptionHandler exceptionHandler)
//        {
//            _next = next;
//            _exceptionHandler = exceptionHandler;
//        }

//        public async Task InvokeAsync(HttpContext httpContext)
//        {
//            try
//            {
//                await _next(httpContext);
//            }
//            catch (Exception ex)
//            {
//                await _exceptionHandler.HandleAsync(httpContext, ex);
//            }
//        }
//    }
//}

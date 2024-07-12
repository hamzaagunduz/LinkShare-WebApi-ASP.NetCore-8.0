using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Link.Application.Common
{
    public class CustomResult<T> : IActionResult
    {
        private readonly T _data;
        private readonly HttpStatusCode _statusCode;

        public CustomResult(T data, HttpStatusCode statusCode)
        {
            _data = data;
            _statusCode = statusCode;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var response = new CustomResponse<T>
            {
                Data = _data,
                Status = (int)_statusCode
            };

            var json = JsonSerializer.Serialize(response);
            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)_statusCode;

            await context.HttpContext.Response.WriteAsync(json);
        }
    }

    public class CustomResponse<T>
    {
        public T Data { get; set; }
        public int Status { get; set; }
    }
}

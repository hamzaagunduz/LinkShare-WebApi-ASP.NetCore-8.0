using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Link.Application.Common
{
    public class CustomResult<T> : IActionResult
    {
        private readonly T _data;
        private readonly HttpStatusCode _statusCode;
        private readonly List<string> _errors;

        public CustomResult(T data, HttpStatusCode statusCode, List<string> errors = null)
        {
            _data = data;
            _statusCode = statusCode;
            _errors = errors;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var response = new CustomResponse<T>
            {
                Data = _data,
                Status = (int)_statusCode,
                Errors = _errors
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
        public List<string> Errors { get; set; }
    }
}

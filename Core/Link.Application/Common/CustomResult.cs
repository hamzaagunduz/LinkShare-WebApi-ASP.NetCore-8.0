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
        public Dictionary<string, List<string>> Errors { get; }

        public CustomResult(T data, HttpStatusCode statusCode, List<string> errors = null, Dictionary<string, List<string>> errorDictionary = null)
        {
            _data = data;
            _statusCode = statusCode;
            Errors = errorDictionary ?? (errors != null ? new Dictionary<string, List<string>> { { "general", errors } } : null);
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var response = new CustomResponse<T>
            {
                Data = _data,
                Status = (int)_statusCode,
                Errors = Errors // Hataları burada döndürün
            };

            // Hata varsa status kodunu 400 Bad Request olarak ayarlayın
            if (Errors != null && Errors.Count > 0)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                context.HttpContext.Response.StatusCode = (int)_statusCode;
            }

            var json = JsonSerializer.Serialize(response);
            context.HttpContext.Response.ContentType = "application/json";
            await context.HttpContext.Response.WriteAsync(json);
        }
    }

    public class CustomResponse<T>
    {
        public T Data { get; set; }
        public int Status { get; set; }
        public Dictionary<string, List<string>> Errors { get; set; }
    }
}

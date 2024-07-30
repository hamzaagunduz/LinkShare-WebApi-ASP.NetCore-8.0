using Link.Dto.ApiResponseDtos;
using Link.Dto.AppUserDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Link.WebUI.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RegisterController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(RegisterDto registerDto)
        {



            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(registerDto);
            var contentString = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync("https://localhost:7048/api/AppUser", contentString);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Login");
            }

            else
            {
                // API'den dönen hataları ModelState'e ekleyin
                var errorResponse = JsonConvert.DeserializeObject<ApiResponseDto<object>>(await responseMessage.Content.ReadAsStringAsync());
                foreach (var error in errorResponse.Errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }

                foreach (var state in ModelState)
                {
                    var key = state.Key;
                    var errors = state.Value.Errors;
                    foreach (var error in errors)
                    {
                        Console.WriteLine($"Key: {key}, Error: {error.ErrorMessage}");
                    }
                }

                // Hatalarla birlikte formu tekrar göster
                return View(registerDto);
            }
        }
    }
}

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
        private readonly RecaptchaService _recaptchaService;


        public RegisterController(IHttpClientFactory httpClientFactory, RecaptchaService recaptchaService)
        {
            _httpClientFactory = httpClientFactory;
            _recaptchaService = recaptchaService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(RegisterDto registerDto)
        {
            var isRecaptchaValid = await _recaptchaService.VerifyRecaptchaAsync(registerDto.RecaptchaToken);
            if (!isRecaptchaValid)
            {
                    ModelState.AddModelError(string.Empty, "reCAPTCHA doğrulaması başarısız oldu. Lütfen tekrar deneyin.");
                    return View(registerDto);
            }


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
                var responseContent = await responseMessage.Content.ReadAsStringAsync();
                var errorResponse = JsonConvert.DeserializeObject<ApiResponseDto<object>>(responseContent);

                if (errorResponse.Errors != null)
                {
                    foreach (var error in errorResponse.Errors)
                    {
                        // Her bir hata anahtar ve mesajlarını ModelState'e ekle
                        foreach (var errorMessage in error.Value)
                        {
                            ModelState.AddModelError(error.Key, errorMessage);
                        }
                    }
                }

                // Hatalarla birlikte formu tekrar göster
                return View(registerDto);
            }
        }
    }
}

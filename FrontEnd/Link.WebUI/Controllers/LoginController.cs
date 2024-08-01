using Link.Dto.AppUserDtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;
using System.Text;
using Link.WebUI.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.Tasks;
using Link.Dto.ApiResponseDtos;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Link.WebUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly RecaptchaService _recaptchaService;

        public LoginController(IHttpClientFactory httpClientFactory, RecaptchaService recaptchaService)
        {
            _httpClientFactory = httpClientFactory;
            _recaptchaService = recaptchaService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // Eylemin içeriği
            return View();
        }

        [HttpPost]
        [EnableRateLimiting("fixed")]
        public async Task<IActionResult> Index(LoginAppUserDto loginAppUserDto)
        {
            // reCAPTCHA doğrulaması
            
            var isRecaptchaValid = await _recaptchaService.VerifyRecaptchaAsync(loginAppUserDto.RecaptchaToken);
            if (!isRecaptchaValid)
            {
                ModelState.AddModelError(string.Empty, "reCAPTCHA doğrulaması başarısız oldu. Lütfen tekrar deneyin.");
                return View(loginAppUserDto);
            }

            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(loginAppUserDto), Encoding.UTF8, "application/json");
            Console.WriteLine(loginAppUserDto.RecaptchaToken);
            var response = await client.PostAsync("https://localhost:7048/api/AppUser/LoginToken", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var tokenModel = System.Text.Json.JsonSerializer.Deserialize<JwtResponseModel>(jsonData, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                if (tokenModel != null)
                {
                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadJwtToken(tokenModel.Token);
                    var claims = token.Claims.ToList();

                    if (tokenModel.Token != null)
                    {
                        claims.Add(new Claim("access_tokena", tokenModel.Token));
                        var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
                        var authProps = new AuthenticationProperties
                        {
                            ExpiresUtc = tokenModel.ExpireDate,
                            IsPersistent = true
                        };

                        Response.Cookies.Append("access_token", tokenModel.Token, new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            Expires = tokenModel.ExpireDate
                        });

                        Console.WriteLine($"Giriş yapan kullanıcının ID'si: {new JwtSecurityTokenHandler().ReadJwtToken(tokenModel.Token).Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value}");

                        Console.WriteLine($"token: {tokenModel.Token}");

                        await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProps);

                        return RedirectToAction("Index", "Profile");
                    }
                }
            }

            else
            {
                var responseContent = await response.Content.ReadAsStringAsync();


                var apiResponse = JsonConvert.DeserializeObject<ApiResponseDto<object>>(responseContent);


                if (apiResponse.Errors != null)
                {
                    foreach (var error in apiResponse.Errors)
                    {
                        // Her bir hata anahtar ve mesajlarını ModelState'e ekle
                        foreach (var errorMessage in error.Value)
                        {
                            ModelState.AddModelError(error.Key, errorMessage);
                        }
                    }
                }

            }



            return View(loginAppUserDto);
        }
    }
}

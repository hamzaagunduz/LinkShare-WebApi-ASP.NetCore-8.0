using Link.Dto.AppUserDtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace Link.WebUI.Controllers
{
    public class UILayoutController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UILayoutController(IHttpClientFactory httpClient)
        {
            _httpClientFactory = httpClient;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CallApi()
        {
            var client = _httpClientFactory.CreateClient();

            // Logout API çağrısı yap
            var responseMessage = await client.PostAsync("https://localhost:7048/api/AppUser/Logout", new StringContent(string.Empty));

            if (responseMessage.IsSuccessStatusCode)
            {
                // Kullanıcı kimlik doğrulaması kontrolü ve güncelleme
                if (User.Identity.IsAuthenticated)
                {
                    Console.WriteLine("Kullanıcı kimlik doğrulaması başarılı.");
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    Console.WriteLine($"Kullanıcı ID'si: {userId}");

                    // Kullanıcıyı kimlik doğrulamasını güncelle
                    await HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);

                    // Çerezleri sil
                    Response.Cookies.Delete("access_token");

                    // Yönlendirme işlemi
                    return RedirectToAction("Index", "Login");
                }
            }
            else
            {
                return View("Error"); // Hata sayfasına yönlendirme
            }

            // Logout işleminden sonra kimlik doğrulama başarısızsa
            return RedirectToAction("Index", "Login");
        }

    }
}

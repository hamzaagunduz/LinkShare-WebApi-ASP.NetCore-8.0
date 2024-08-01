using Link.Application.Common;
using Link.Dto.ApiResponseDtos;
using Link.Dto.SettingsDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Link.WebUI.Controllers
{
    public class ProfileSettingsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProfileSettingsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7048/api/AppUser/{userId}");
            var token = Request.Cookies["access_token"];


                if (response.IsSuccessStatusCode&&!string.IsNullOrEmpty(token) && User.Identity.IsAuthenticated)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<ApiResponseDto<GetSettingsDto>>(jsonData);

                        var viewModel = new SettingsViewModel
                        {
                            GetSettings = values.Data,
                            UpdateSettings = new UpdateSettingsDto
                            {
                                Id=values.Data.Id,
                                firstName = values.Data.FirstName,
                                userName = values.Data.UserName,
                                surName = values.Data.SurName,
                                email = values.Data.Email,
                                about = values.Data.About,
                                password = values.Data.Password // Şifreyi almanız gerekebilir, güvenliğini sağlayın
                            }

                        };
                    return View(viewModel);

                }
            else
                {
                return RedirectToAction("Index","Login");

            }




        }









        [HttpPost]
        public async Task<IActionResult> Index(SettingsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var client = _httpClientFactory.CreateClient();
            var jsonContent = JsonConvert.SerializeObject(viewModel.UpdateSettings);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("https://localhost:7048/api/AppUser", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<ApiResponseDto<object>>(jsonData);

            if (apiResponse.Errors != null)
            {
                foreach (var error in apiResponse.Errors)
                {
                    foreach (var message in error.Value)
                    {
                        ModelState.AddModelError(error.Key, message);
                    }
                }
            }


            return View(viewModel);
        }
    }
}

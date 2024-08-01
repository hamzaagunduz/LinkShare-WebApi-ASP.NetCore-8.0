using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json; // JSON dönüşümü için kullanılır
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Link.Dto.CommentDtos;
using Link.Dto.ApiResponseDtos;
using System.Text;
using System.Security.Claims;
using Link.Dto.SettingsDtos;

namespace Link.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 5)
        {

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7048/api/Comment/GetCommentAndAnswers?page={page}&pageSize={pageSize}");

            if (response.IsSuccessStatusCode)
            {


                var jsonResponse = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponseDto<List<CommentAndAnswerDto>>>(jsonResponse);

                return View(apiResponse.Data);

            }
            else
            {
                // Hata mesajını view'e gönderin veya uygun bir hata işlemi gerçekleştirin
                ViewBag.ErrorMessage = "API isteği başarısız.";
                return View(new List<CommentAndAnswerDto>());
            }
        }


        public async Task<IActionResult> LoadMoreComments(int page = 1, int pageSize = 5)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7048/api/Comment/GetCommentAndAnswers?page={page}&pageSize={pageSize}");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponseDto<List<CommentAndAnswerDto>>>(jsonResponse);

                if (apiResponse.Data != null && apiResponse.Data.Count > 0)
                {
                    return PartialView("Components/_CommentAndAnswerPartial/Default", apiResponse.Data);
                }
                else
                {
                    return NoContent(); // Boş veri döndüğünde NoContent durumu döner
                }
            }
            else
            {
                return StatusCode((int)response.StatusCode, "API isteği başarısız.");
            }
        }




    }
}

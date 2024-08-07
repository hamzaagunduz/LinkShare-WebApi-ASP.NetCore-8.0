using Link.Dto.ApiResponseDtos;
using Link.Dto.CommentDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Link.WebUI.Controllers
{
    public class TrendController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TrendController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet("Trend")]
        public async Task<IActionResult> Index(int topCount = 5)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7048/api/Comment/GetTopLikedCommentsWithAnswers?topCount={topCount}");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponseDto<List<CommentAndAnswerDto>>>(jsonResponse);

                if (apiResponse != null)
                {
                    return View(apiResponse.Data);
                }
                else
                {
                    // API yanıtı beklenen formatta değilse uygun bir hata işlemi gerçekleştirin
                    ViewBag.ErrorMessage = "API yanıtı beklenen formatta değil.";
                    return View(new List<CommentAndAnswerDto>());
                }
            }
            else
            {
                // Hata mesajını view'e gönderin veya uygun bir hata işlemi gerçekleştirin
                ViewBag.ErrorMessage = "API isteği başarısız.";
                return View(new List<CommentAndAnswerDto>());
            }
        }
    }
}

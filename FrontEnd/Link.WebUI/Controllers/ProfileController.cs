using Link.Dto.ApiResponseDtos;
using Link.Dto.CommentDtos;
using Link.Dto.LinkDto;
using Link.Dto.ProfileDtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace Link.WebUI.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProfileController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!string.IsNullOrEmpty(userId))
            {
                var linksTask = client.GetAsync($"https://localhost:7048/api/Links/{userId}");
                var commentsTask = client.GetAsync($"https://localhost:7048/api/Comment/GetCommentsWithAppUser/{userId}");
                var appUserTask = client.GetAsync($"https://localhost:7048/api/AppUser/{userId}");


                await Task.WhenAll(linksTask, commentsTask,appUserTask);

                var linksResponse = await linksTask;
                var commentsResponse = await commentsTask;
                var userResponse = await appUserTask;

                if (linksResponse.IsSuccessStatusCode && commentsResponse.IsSuccessStatusCode&& userResponse.IsSuccessStatusCode)
                {
                    var linksJsonData = await linksResponse.Content.ReadAsStringAsync();
                    var commentsJsonData = await commentsResponse.Content.ReadAsStringAsync();
                    var userJsonData = await userResponse.Content.ReadAsStringAsync();


                    var linksApiResponse = JsonConvert.DeserializeObject<ApiResponseDto<List<GetLinkDto>>>(linksJsonData);
                    var commentsApiResponse = JsonConvert.DeserializeObject<ApiResponseDto<List<CommentDto>>>(commentsJsonData);
                    var userApiResponse= JsonConvert.DeserializeObject<ApiResponseDto<GetAppUserDto>>(userJsonData);


                    var combinedResponse = new CombinedResponseDto
                    {
                        Links = linksApiResponse.Data,
                        Comments = commentsApiResponse.Data,
                        CommentAnswers = new Dictionary<int, List<AnswerDto>>(),
                        GetAppUserDto=userApiResponse.Data
                    };

                    foreach (var comment in combinedResponse.Comments)
                    {
                        var answersResponse = await GetAnswersWithId(comment.ProfileCommentID);
                        combinedResponse.CommentAnswers[comment.ProfileCommentID] = answersResponse.Data;
                    }

                    return View(combinedResponse);
                }
            }

            return View(new CombinedResponseDto
            {
                Links = new List<GetLinkDto>(),
                Comments = new List<CommentDto>(),
                CommentAnswers = new Dictionary<int, List<AnswerDto>>()
            });
        }


        private async Task<ApiResponseDto<List<AnswerDto>>> GetAnswersWithId(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7048/api/Comment/GetAnswersWithId/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ApiResponseDto<List<AnswerDto>>>(jsonData);
            }

            return new ApiResponseDto<List<AnswerDto>> { Data = new List<AnswerDto>() }; // Boş bir liste döndür
        }



        [HttpPost]

        public async Task<IActionResult> AddLink(AddLinkDto linkDto)
        {

            var client = _httpClientFactory.CreateClient();
            //var tokenclaim = await HttpContext.GetTokenAsync(JwtBearerDefaults.AuthenticationScheme, "access_tokena");
            var token = Request.Cookies["access_token"];
            if (!string.IsNullOrEmpty(token))
                
            {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    var jsonContent = JsonConvert.SerializeObject(linkDto);
                    var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    var responseMessage = await client.PostAsync("https://localhost:7048/api/Links", contentString);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Bağlantı eklenirken bir hata oluştu.");
                    }
                }  

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(AddCommentDto commentDto)
        {
            var client = _httpClientFactory.CreateClient();
            var token = Request.Cookies["access_token"];
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                commentDto.AppUserID = 10;
                var jsonContent = JsonConvert.SerializeObject(commentDto);
                var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var responseMessage = await client.PostAsync("https://localhost:7048/api/Comment", contentString);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", new {id= commentDto.AppUserID });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Yorum eklenirken bir hata oluştu.");
                }
            }

            return RedirectToAction("Index");
        }



        [HttpPost]
        public async Task<IActionResult> AddAnswer(AddAnswerDto answerDto)
        {
            var client = _httpClientFactory.CreateClient();
            var token = Request.Cookies["access_token"];
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var jsonContent = JsonConvert.SerializeObject(answerDto);
                var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var responseMessage = await client.PostAsync("https://localhost:7048/api/Comment/CreateAnswer", contentString);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Cevap eklenirken bir hata oluştu.");
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveLink(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7048/api/Links?id={id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveComment(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7048/api/Comment?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");

            }
            return View();
        }

    }
}

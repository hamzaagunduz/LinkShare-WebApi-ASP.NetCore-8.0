using Link.Dto.ApiResponseDtos;
using Link.Dto.CommentDtos;
using Link.Dto.LinkDto;
using Link.Dto.ProfileDtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace Link.WebUI.Controllers
{
    public class ShareProfileController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ShareProfileController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var client = _httpClientFactory.CreateClient();



                var profileLinksTask = client.GetAsync($"https://localhost:7048/api/Links/{id}");
                var profileCommentsTask = client.GetAsync($"https://localhost:7048/api/Comment/GetCommentsWithAppUser/{id}");
                var appUserTask = client.GetAsync($"https://localhost:7048/api/AppUser/{id}");


            await Task.WhenAll(profileLinksTask, profileCommentsTask,appUserTask);

                var profileLinksResponse = await profileLinksTask;
                var profileCommentsResponse = await profileCommentsTask;
                 var userResponse = await appUserTask;

            if (profileLinksResponse.IsSuccessStatusCode && profileCommentsResponse.IsSuccessStatusCode&&userResponse.IsSuccessStatusCode)
                {
                    var profileLinksJsonData = await profileLinksResponse.Content.ReadAsStringAsync();
                    var profileCommentsJsonData = await profileCommentsResponse.Content.ReadAsStringAsync();
                    var userJsonData = await userResponse.Content.ReadAsStringAsync();


                    var profileLinksApiResponse = JsonConvert.DeserializeObject<ApiResponseDto<List<GetLinkDto>>>(profileLinksJsonData);
                    var profileCommentsApiResponse = JsonConvert.DeserializeObject<ApiResponseDto<List<CommentDto>>>(profileCommentsJsonData);
                    var userApiResponse = JsonConvert.DeserializeObject<ApiResponseDto<GetAppUserDto>>(userJsonData);

                var combinedResponse = new CombinedResponseDto
                    {
                        Links = profileLinksApiResponse.Data,
                        Comments = profileCommentsApiResponse.Data,
                        CommentAnswers = new Dictionary<int, List<AnswerDto>>(),
                        GetAppUserDto = userApiResponse.Data

                };

                    foreach (var comment in combinedResponse.Comments)
                    {
                        var answersResponse = await GetAnswersWithId(comment.ProfileCommentID);
                        combinedResponse.CommentAnswers[comment.ProfileCommentID] = answersResponse.Data;
                    }

                    return View(combinedResponse);
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

            return new ApiResponseDto<List<AnswerDto>> { Data = new List<AnswerDto>() };
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(AddCommentDto commentDto, int id)
        {
            var client = _httpClientFactory.CreateClient();
            var token = Request.Cookies["access_token"];
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                commentDto.AppUserID = id;
                var jsonContent = JsonConvert.SerializeObject(commentDto);
                var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var responseMessage = await client.PostAsync("https://localhost:7048/api/Comment", contentString);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Yorum eklenirken bir hata oluştu.");
                }
            }

            return RedirectToAction("Index");
        }
    }
}

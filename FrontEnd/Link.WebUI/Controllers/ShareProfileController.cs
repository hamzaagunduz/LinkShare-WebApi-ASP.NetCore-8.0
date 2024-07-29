﻿using FluentValidation;
using Link.Application.Features.Mediator.Validations.LinkValidation;
using Link.Dto.ApiResponseDtos;
using Link.Dto.CommentDtos;
using Link.Dto.LinkDto;
using Link.Dto.ProfileDtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var profileLinksTask = client.GetAsync($"https://localhost:7048/api/Links/{id}");
            var profileCommentsTask = client.GetAsync($"https://localhost:7048/api/Comment/GetCommentsWithAppUser/{id}");
            var appUserTask = client.GetAsync($"https://localhost:7048/api/AppUser/{id}");

            await Task.WhenAll(profileLinksTask, profileCommentsTask, appUserTask);

            var profileLinksResponse = await profileLinksTask;
            var profileCommentsResponse = await profileCommentsTask;
            var userResponse = await appUserTask;

            if (profileLinksResponse.IsSuccessStatusCode && profileCommentsResponse.IsSuccessStatusCode && userResponse.IsSuccessStatusCode)
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
                    GetAppUserDto = userApiResponse.Data,
                    ShowForm = (userId == id.ToString()) // Formu gösterme koşulu
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
                CommentAnswers = new Dictionary<int, List<AnswerDto>>(),
                ShowForm = false
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
                    // Yönlendirme sırasında id parametresini iletin
                    return RedirectToAction("Index", new { id = commentDto.AppUserID });
                }

                else
                {
                    ModelState.AddModelError(string.Empty, "Yorum eklenirken bir hata oluştu.");
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddLink(AddLinkDto linkDto)
        {
            var client = _httpClientFactory.CreateClient();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var token = Request.Cookies["access_token"];
            var validator = new CreateLinkValidation();
            var validationResult = validator.Validate(linkDto);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                // Validasyon hatalarını kullanıcıya göstermek için formu tekrar göster
                return RedirectToAction("Index", new { id = userId });
            }



            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var jsonContent = JsonConvert.SerializeObject(linkDto);
                var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var responseMessage = await client.PostAsync("https://localhost:7048/api/Links", contentString);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", new { id = userId });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Bağlantı eklenirken bir hata oluştu.");
                }
            }

            return RedirectToAction("Index", new { id = userId });
        }


        [HttpPost]
        public async Task<IActionResult> AddAnswer(AddAnswerDto answerDto)
        {
            var client = _httpClientFactory.CreateClient();
            var token = Request.Cookies["access_token"];
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var jsonContent = JsonConvert.SerializeObject(answerDto);
                var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var responseMessage = await client.PostAsync("https://localhost:7048/api/Comment/CreateAnswer", contentString);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", new { id = userId });

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Cevap eklenirken bir hata oluştu.");
                }
            }

            return RedirectToAction("Index", new { id = userId });
        }

        public async Task<IActionResult> RemoveLink(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7048/api/Links?id={id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", new { id = userId });

            }
            return RedirectToAction("Index", new { id = userId });
        }

        public async Task<IActionResult> RemoveComment(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7048/api/Comment?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", new { id = userId });

            }
            return RedirectToAction("Index", new { id = userId });
        }




    }
}
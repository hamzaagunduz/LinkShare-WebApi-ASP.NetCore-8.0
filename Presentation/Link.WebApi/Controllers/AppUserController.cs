using Link.Application.Common;
using Link.Application.Features.Mediator.Commands.AppUserCommands;
using Link.Application.Features.Mediator.Queries.AppUserQueries;
using Link.Application.Tools;
using Link.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace Link.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;


        public AppUserController(IMediator mediator, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _mediator = mediator;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> AppUserList()
        {
            var query = new GetAppUserQuery();
            var result = await _mediator.Send(query);
            return result;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppUser(int id)
        {

            var query = new GetByIdAppUserQuery(id);
            var result = await _mediator.Send(query);
            return result;

        }
        [HttpGet("GetRandomUser")]
        public async Task<IActionResult> GetRandomUser(int count)
        {

            var query = new GetRandomUsersQuery(count);
            var result = await _mediator.Send(query);
            return result;

        }


        [HttpGet("confirmemail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("Invalid user");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return Redirect("https://localhost:7132/login/index"); // Ana sayfa URL'sini buraya yazın
            }

            return BadRequest("Error confirming email");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppUser(CreateAppUserCommand command)
        {
            var result = await _mediator.Send(command);
            
            return result;



        }
        [HttpDelete]
        public async Task<IActionResult> RemoveAppUser(int id)
        {

            var query = new RemoveAppUserCommand(id);
            var result = await _mediator.Send(query);
            return result;


        }
        [HttpPut]
        public async Task<IActionResult> UpdateAppUser(UpdateAppUserCommand command)
        {
            var result = await _mediator.Send(command);
            return result;


        }


        [HttpPost("LoginToken")]
        public async Task<IActionResult> LoginToken(GetCheckAppUserQuery query)
        {
            var values = await _mediator.Send(query);
            if (values.IsExist)
            {
                return Created("", JwtTokenGenerator.GenerateToken(values));
            }
            else
            {

                var errorMessages = new List<string> { "Kullanıcı Adı veya Şifre Hatalı" };

                var errorDictionary = new Dictionary<string, List<string>>
                {
                    { "password", errorMessages }
                };

                // CustomResult ile hata mesajlarını ve status kodunu döndürün
                return new CustomResult<string>(null, HttpStatusCode.BadRequest, null, errorDictionary);
            }
        }

        [HttpGet("GetUserProfile")]

        public IActionResult GetUserProfile()
        {
            
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            return Ok(new { UserId = userId, Username = User.Identity.Name });
        }



        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            Response.Cookies.Delete("access_token");

            return Ok(new { Message = "User logged out successfully." });
        }

        [HttpGet("SearchUsers")]
        public async Task<IActionResult> SearchUsers([FromQuery] string query)
        {
            var searchQuery = new SearchUsersQuery { Query = query };
            var result = await _mediator.Send(searchQuery);
            return result;
        }
    }
}

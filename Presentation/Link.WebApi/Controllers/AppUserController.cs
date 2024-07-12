using Link.Application.Features.Mediator.Commands.AppUserCommands;
using Link.Application.Features.Mediator.Queries.AppUserQueries;
using Link.Application.Tools;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Link.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppUserController(IMediator mediator)
        {
            _mediator = mediator;
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
                return BadRequest("Kullanıcı adı veya şifre hatalıdır");
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
    }
}

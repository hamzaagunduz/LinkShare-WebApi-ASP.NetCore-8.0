using Link.Application.Features.Mediator.Commands.FollowCommands;
using Link.Application.Features.Mediator.Queries.CommentQueries;
using Link.Application.Features.Mediator.Queries.FollowQueries;
using Link.Application.Features.Mediator.Queries.LinkQueries;
using Link.Application.Features.Mediator.Results.FollowResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Link.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FollowController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("follow")]
        public async Task<IActionResult> Follow([FromBody] CreateFollowCommand command)
        {
            var result = await _mediator.Send(command);
            return result;

        }

        [HttpPost("unfollow")]
        public async Task<IActionResult> Unfollow([FromBody] UnfollowUserCommand command)
        {
            var result = await _mediator.Send(command);
            return result;

        }


        [HttpGet("GetFollowersById/{id}")]
        public async Task<IActionResult> GetFollowers(int id)
        {
            var query = new GetByAppUserIdFollowersQuery(id);
            var result = await _mediator.Send(query);
            return result;

        }

        [HttpGet("GetFollowingById/{id}")]
        public async Task<IActionResult> GetFollowing(int id)
        {
            var query = new GetByAppUserIdFollowingQuery(id);
            var result = await _mediator.Send(query);
            return result;

        }

        [HttpGet("CheckFollowStatus")]
        public async Task<IActionResult> CheckFollowStatus([FromQuery] int followingUserId)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var loginUserId = int.Parse(userIdClaim.Value);

            var isFollowing = await _mediator.Send(new CheckFollowStatusQuery(loginUserId, followingUserId));

            return Ok(new { isFollowing });
        }



    }
}

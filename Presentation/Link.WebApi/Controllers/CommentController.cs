using Link.Application.Common;
using Link.Application.Features.Mediator.Commands.Comment;
using Link.Application.Features.Mediator.Commands.FollowCommands;
using Link.Application.Features.Mediator.Queries.AppUserQueries;
using Link.Application.Features.Mediator.Queries.CommentQueries;
using Link.Application.Features.Mediator.Queries.FollowQueries;
using Link.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace Link.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommentController(IMediator mediator)
        {
            _mediator = mediator;
        }



        //[HttpPost("Comment")]
        //public async Task<IActionResult> Comment([FromBody] CreateCommentCommand command)
        //{
        //    var result = await Sender.Send(command);

        //    return result;
        //}
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommentCommand request)
        {
            
            var result = await _mediator.Send(request);
            if (result.IsSucceed == false)
                return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpGet("GetByAppUserIDCommentQuery/{id}")]//kullanıcı profilindeki yorum
        public async Task<IActionResult> GetByAppUserIDCommentQuery(int id)
        {
            var query = new GetByAppUserIDCommentQuery(id);
            var result = await _mediator.Send(query);
            return result;
        }

        [HttpGet("GetByWriterIDCommentQuery/{id}")]//yaptığı yorumlar
        public async Task<IActionResult> GetFollowers(int id)
        {
            var query = new GetByWriterIDCommentQuery(id);
            var result = await _mediator.Send(query);
            return result;

        }
    }
}

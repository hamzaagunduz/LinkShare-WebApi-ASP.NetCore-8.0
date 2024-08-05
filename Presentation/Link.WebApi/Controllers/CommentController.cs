using Link.Application.Common;
using Link.Application.Features.Mediator.Commands.AnswerCommands;
using Link.Application.Features.Mediator.Commands.Comment;
using Link.Application.Features.Mediator.Commands.CommentCommands;
using Link.Application.Features.Mediator.Commands.FollowCommands;
using Link.Application.Features.Mediator.Commands.LikeCommands;
using Link.Application.Features.Mediator.Commands.LinkCommands;
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




        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommentCommand request)
        {
            var result = await _mediator.Send(request);
            return result;
        }
        [HttpPost("CreateAnswer")]
        public async Task<IActionResult> CreateAnswer([FromBody] CreateAnswerCommand request)
        {
            var result = await _mediator.Send(request);
            return result;
        }


        [HttpDelete]
        public async Task<IActionResult> RemoveComment(int id)
        {
            await _mediator.Send(new RemoveCommentCommand(id));
            return Ok("Yorum silindi");
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

        [HttpGet("GetCommentsWithAppUser/{id}")]//yaptığı yorumlar
        public async Task<IActionResult> GetCommentsWithAppUser(int id)
        {
            var query = new GetCommentsWithAppUserQuery(id);
            var result = await _mediator.Send(query);
            return result;

        }
        [HttpGet("GetAnswersWithId/{id}")]//yaptığı yorumlar
        public async Task<IActionResult> GetAnswersWithId(int id)
        {
            var query = new GetAnswersForCommentQuery(id);
            var result = await _mediator.Send(query);
            return result;

        }

        [HttpGet("GetCommentAndAnswers")]//yaptığı yorumlar
        public async Task<IActionResult> GetCommentAndAnswers([FromQuery] int page, [FromQuery] int pageSize)
        {
            var query = new GetCommentAndAnwerQuery(page,pageSize);
            var result = await _mediator.Send(query);
            return result;

        }
        [HttpPost("CreateLike")]
        public async Task<IActionResult> CreateLike([FromBody] CreateLikeCommand request)
        {
            var result = await _mediator.Send(request);
            return result;
        }


    }
}

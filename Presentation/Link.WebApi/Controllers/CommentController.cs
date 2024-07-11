﻿using Link.Application.Features.Mediator.Commands.Comment;
using Link.Application.Features.Mediator.Commands.FollowCommands;
using Link.Application.Features.Mediator.Queries.CommentQueries;
using Link.Application.Features.Mediator.Queries.FollowQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("Comment")]
        public async Task<IActionResult> Comment([FromBody] CreateCommentCommand command)
        {
            await _mediator.Send(command);
            return Ok("Yorum başarıyla eklendi");

        }

        [HttpGet("GetByAppUserIDCommentQuery/{id}")]//kullanıcı profilindeki yorum
        public async Task<IActionResult> GetByAppUserIDCommentQuery(int id)
        {
            var value = await _mediator.Send(new GetByAppUserIDCommentQuery(id));
            return Ok(value);
        }

        [HttpGet("GetByWriterIDCommentQuery/{id}")]//yaptığı yorumlar
        public async Task<IActionResult> GetFollowers(int id)
        {
            var value = await _mediator.Send(new GetByWriterIDCommentQuery(id));
            return Ok(value);
        }
    }
}
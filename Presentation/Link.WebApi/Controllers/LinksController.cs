﻿using Link.Application.Features.Mediator.Commands.AppUserCommands;
using Link.Application.Features.Mediator.Commands.LinkCommands;
using Link.Application.Features.Mediator.Handlers.LinkHandlers;
using Link.Application.Features.Mediator.Queries.AppUserQueries;
using Link.Application.Features.Mediator.Queries.LinkQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Link.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LinksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLink(CreateLinkCommand command)
        {

            await _mediator.Send(command);
            return Ok("Link başarıyla eklendi");

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByUserIDLink(int id)
        {
            var value = await _mediator.Send(new GetByUserIdLinkQuery(id));
            return Ok(value);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveLink(int id)
        {
            await _mediator.Send(new RemoveLinkCommand(id));
            return Ok("AppUser başarıyla silindi");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateLink(UpdateLinkCommand command)
        {
            await _mediator.Send(command);
            return Ok("AppUser başarıyla güncellendi");
        }

    }
}
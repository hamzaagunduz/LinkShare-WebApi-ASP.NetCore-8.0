﻿using Link.Application.Features.Mediator.Commands.AppUserCommands;
using Link.Application.Features.Mediator.Queries.AppUserQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            var values = await _mediator.Send(new GetAppUserQuery());
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppUser(int id)
        {
            var value = await _mediator.Send(new GetByIdAppUserQuery(id));
            return Ok(value);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAppUser(CreateAppUserCommand command)
        {

            await _mediator.Send(command);
            return Ok("AppUser başarıyla eklendi");

        }
        [HttpDelete]
        public async Task<IActionResult> RemoveAppUser(int id)
        {
            await _mediator.Send(new RemoveAppUserCommand(id));
            return Ok("AppUser başarıyla silindi");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAppUser(UpdateAppUserCommand command)
        {
            await _mediator.Send(command);
            return Ok("AppUser başarıyla güncellendi");
        }
    }
}
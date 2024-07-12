﻿using Link.Application.Common;
using Link.Application.Features.Mediator.Commands.AppUserCommands;
using Link.Application.Interfaces;
using Link.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Handlers.AppUserHandlers
{
    public class CreateAppUserCommandHandler : IRequestHandler<CreateAppUserCommand, CustomResult<AppUser>>
    {
        private readonly UserManager<AppUser> _userManager;

        public CreateAppUserCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CustomResult<AppUser>> Handle(CreateAppUserCommand request, CancellationToken cancellationToken)
        {
            var newUser = new AppUser
            {
                FirstName = request.FirstName,
                UserName = request.UserName,
                SurName = request.SurName,
                Email = request.Email,
                Password = request.Password,
                About = request.About,
                FollowersCount = request.FollowersCount,
                FollowingCount = request.FollowingCount,
                PostCount = request.PostCount,
                View = request.View,
            };

            IdentityResult result = await _userManager.CreateAsync(newUser, request.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => $"{e.Code}: {e.Description}").ToList();
                Debug.WriteLine("Kullanıcı oluşturulurken hata oluştu:");
                errors.ForEach(error => Debug.WriteLine(error));

                return new CustomResult<AppUser>(newUser, HttpStatusCode.BadRequest);
            }

            return new CustomResult<AppUser>(newUser, HttpStatusCode.OK);
        }
    }
}

using FluentValidation.Results;
using Link.Application.Common;
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
    public class CreateAppUserCommandHandler : IRequestHandler<CreateAppUserCommand, CustomResult<string>>
    {
        private readonly UserManager<AppUser> _userManager;

        public CreateAppUserCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CustomResult<string>> Handle(CreateAppUserCommand request, CancellationToken cancellationToken)
        {
            var newUser = new AppUser
            {
                FirstName = request.FirstName,
                UserName = request.UserName,
                SurName = request.SurName,
                Email = request.Email,
                Password = request.Password,
                About = "Merhaba yeni üye oldum",
                FollowersCount = 0,
                FollowingCount = 0,
                PostCount = 0,
                View = 0,
            };

            IdentityResult result = await _userManager.CreateAsync(newUser, request.Password);


            return new CustomResult<string>("Link Oluşturma successfully.", HttpStatusCode.OK);
        }
    }
}

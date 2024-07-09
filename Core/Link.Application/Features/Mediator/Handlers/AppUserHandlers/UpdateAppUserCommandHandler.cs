using Link.Application.Features.Mediator.Commands.AppUserCommands;
using Link.Application.Interfaces;
using Link.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Handlers.AppUserHandlers
{
    public class UpdateAppUserCommandHandler : IRequestHandler<UpdateAppUserCommand>
    {
        private readonly IRepository<AppUser> _repository;
        private readonly UserManager<AppUser> _userManager;

        public UpdateAppUserCommandHandler(IRepository<AppUser> repository, UserManager<AppUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;

        }

        public async Task Handle(UpdateAppUserCommand request, CancellationToken cancellationToken)
        {
            var values = await _repository.GetByIdAsync(request.AppUserID);
            values.FirstName = request.FirstName;
            values.SurName = request.SurName;
            values.UserName = request.UserName;
            values.About = request.About;
            values.Email = request.Email;
            values.FollowersCount = request.FollowersCount;
            values.FollowingCount = request.FollowingCount;
            values.PostCount = request.PostCount;
            values.Password = request.Password;
            values.View = request.View;
            values.ImageUrl = request.ImageUrl;
            values.About = request.About;

            var removePasswordResult = await _userManager.RemovePasswordAsync(values);
            var addPasswordResult = await _userManager.AddPasswordAsync(values, request.Password);

            await _repository.UpdateAsync(values);
        }
    }
}

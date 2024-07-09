using Link.Application.Features.Mediator.Commands.AppUserCommands;
using Link.Application.Interfaces;
using Link.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Handlers.AppUserHandlers
{
    public class CreateAppUserCommandHandler : IRequestHandler<CreateAppUserCommand>
    {
        private readonly IRepository<AppUser> _repository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMediator _mediator;

        public CreateAppUserCommandHandler(IRepository<AppUser> repository, UserManager<AppUser> userManager, IMediator mediator)
        {
            _repository = repository;
            _userManager = userManager;
            _mediator = mediator;
        }

        public async Task Handle(CreateAppUserCommand request, CancellationToken cancellationToken)
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
                // Hata mesajlarını debug loguna yaz
                Debug.WriteLine("Kullanıcı oluşturulurken hata oluştu:");
                foreach (var error in result.Errors)
                {
                    Debug.WriteLine($"Kod: {error.Code}, Açıklama: {error.Description}");
                }
            }
        }
    }
}

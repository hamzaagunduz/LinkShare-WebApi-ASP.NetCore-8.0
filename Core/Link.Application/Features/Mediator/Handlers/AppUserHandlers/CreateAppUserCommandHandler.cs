using FluentValidation.Results;
using Link.Application.Common;
using Link.Application.Features.Mediator.Commands.AppUserCommands;
using Link.Application.Interfaces;
using Link.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;

        public CreateAppUserCommandHandler(UserManager<AppUser> userManager, IConfiguration configuration, IEmailSender emailSender)
        {
            _userManager = userManager;
            _configuration = configuration;
            _emailSender = emailSender;
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

            if (result.Succeeded)
            {
                // E-posta onayı için e-posta gönder
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                var confirmationLink = $"https://localhost:7048/api/AppUser/confirmemail?userId={Uri.EscapeDataString(newUser.Id.ToString())}&token={Uri.EscapeDataString(token)}";


                await _emailSender.SendEmailAsync(newUser.Email, "Email Confirmation", $"Please confirm your email by clicking <a href='{confirmationLink}'>here</a>.");

                return new CustomResult<string>("User created successfully. Please check your email to confirm your account.", HttpStatusCode.OK);
            }

            return new CustomResult<string>("User creation failed.", HttpStatusCode.BadRequest);
        }
    }

}

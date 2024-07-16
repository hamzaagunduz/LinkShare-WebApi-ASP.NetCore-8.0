using FluentValidation;
using FluentValidation.Results;
using Link.Application.Common;
using Link.Application.Features.Mediator.Commands.AppUserCommands;
using Link.Application.FluentValidations;
using Link.Application.Interfaces;
using Link.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Handlers.AppUserHandlers
{
    public class UpdateAppUserCommandHandler : IRequestHandler<UpdateAppUserCommand, CustomResult<AppUser>>
    {
        private readonly IRepository<AppUser> _repository;
        private readonly UserManager<AppUser> _userManager;

        public UpdateAppUserCommandHandler(IRepository<AppUser> repository, UserManager<AppUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public async Task<CustomResult<AppUser>> Handle(UpdateAppUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(request.AppUserID);

            if (user == null)
            {
                return new CustomResult<AppUser>(null, HttpStatusCode.NotFound);
            }

            try
            {
                // Update user properties
                user.FirstName = request.FirstName;
                user.SurName = request.SurName;
                user.UserName = request.UserName;
                user.About = request.About;
                user.Email = request.Email;
                user.FollowersCount = request.FollowersCount;
                user.FollowingCount = request.FollowingCount;
                user.PostCount = request.PostCount;
                user.View = request.View;
                user.ImageUrl = request.ImageUrl;

                // Validate user
                var validator = new AppUserValidator(); // Assuming you have a validator class
                ValidationResult validationResult = await validator.ValidateAsync(user, cancellationToken);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return new CustomResult<AppUser>(null, HttpStatusCode.BadRequest, errors);
                }



                // Remove old password and set new password
                var removePasswordResult = await _userManager.RemovePasswordAsync(user);
                var addPasswordResult = await _userManager.AddPasswordAsync(user, request.Password);

                // Update user in repository
                await _repository.UpdateAsync(user);

                return new CustomResult<AppUser>(user, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                // Handle exception, log the error, etc.
                return new CustomResult<AppUser>(null, HttpStatusCode.InternalServerError);
            }
        }
    }
}

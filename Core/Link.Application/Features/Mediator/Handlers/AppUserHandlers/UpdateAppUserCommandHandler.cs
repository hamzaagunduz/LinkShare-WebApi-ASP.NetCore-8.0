using FluentValidation;
using FluentValidation.Results;
using Link.Application.Common;
using Link.Application.Features.Mediator.Commands.AppUserCommands;
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
    public class UpdateAppUserCommandHandler : IRequestHandler<UpdateAppUserCommand, CustomResult<string>>
    {
        private readonly IRepository<AppUser> _repository;
        private readonly UserManager<AppUser> _userManager;

        public UpdateAppUserCommandHandler(IRepository<AppUser> repository, UserManager<AppUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public async Task<CustomResult<string>> Handle(UpdateAppUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByIdAsync(request.Id);

            if (user == null)
            {
                return new CustomResult<string>(null, HttpStatusCode.NotFound);
            }

            try
            {

                if (await _userManager.CheckPasswordAsync(user, request.Password))
                {

                    user.FirstName = request.FirstName;
                    user.SurName = request.SurName;
                    user.UserName = request.UserName;
                    user.About = request.About;
                    user.Email = request.Email;
                    user.ImageUrl = request.ImageUrl;
                    await _repository.UpdateAsync(user);

                    return new CustomResult<string>("güncelleme yapıldı.", HttpStatusCode.OK);



                }



                var errorMessages = new List<string> { "Şifre hatalı" };

                var errorDictionary = new Dictionary<string, List<string>>
                {
                    { "password", errorMessages }
                };

                // CustomResult ile hata mesajlarını ve status kodunu döndürün
                return new CustomResult<string>(null, HttpStatusCode.BadRequest, null, errorDictionary);


            }
            catch (Exception ex)
            {
                // Handle exception, log the error, etc.
                return new CustomResult<string>(null, HttpStatusCode.InternalServerError);
            }
        }
    }
}

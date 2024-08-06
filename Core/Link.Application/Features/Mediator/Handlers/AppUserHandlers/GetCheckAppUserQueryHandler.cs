using Link.Application.Features.Mediator.Queries.AppUserQueries;
using Link.Application.Features.Mediator.Results.AppUserResults;
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
    public class GetCheckAppUserQueryHandler : IRequestHandler<GetCheckAppUserQuery, GetCheckAppUserQueryResult>
    {

        private readonly UserManager<AppUser> _userManager;

        public GetCheckAppUserQueryHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<GetCheckAppUserQueryResult> Handle(GetCheckAppUserQuery request, CancellationToken cancellationToken)
        {
            var values = new GetCheckAppUserQueryResult();
   
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                values.IsExist = false;
            }
            else
            {
                var passwordVerificationResult = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

                if (passwordVerificationResult == PasswordVerificationResult.Success) {
                    values.IsExist = true;
                    values.UserName = user.UserName;
                    values.Id = user.Id;
                    var userRoles = await _userManager.GetRolesAsync(user);
                }
                else
                {

                }

            }
            return values;
        }
    }
}

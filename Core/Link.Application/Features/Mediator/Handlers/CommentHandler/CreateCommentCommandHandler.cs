using Link.Application.Features.Mediator.Commands.Comment;
using Link.Application.Interfaces;
using Link.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Handlers.CommentHandler
{
    public class CreateCommentCommandHandler: IRequestHandler<CreateCommentCommand>
    {
        private readonly IRepository<ProfileComment> _repository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public CreateCommentCommandHandler(IRepository<ProfileComment> repositor, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repositor;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims
    .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

            if (userIdClaim == null)
            {
                // Handle the case where the user ID claim is not found
                throw new UnauthorizedAccessException("User ID claim not found in token.");
            }
            var followerUser = await _userManager.FindByIdAsync(request.AppUserID.ToString());

            var following = new ProfileComment
            {
                AppUserID = request.AppUserID,
                WriterID = int.Parse(userIdClaim.Value),
                View = request.View,
                Hidden = request.Hidden,
                Comment = request.Comment,
                Like = request.Like,
                Time = DateTime.Now,
        
            };

            await _repository.CreateAsync(following);
        }
    }
}

using Link.Application.Common;
using Link.Application.Features.Mediator.Commands.Comment;
using Link.Application.Interfaces;
using Link.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Handlers.CommentHandler
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CustomResult<string>>
    {
        private readonly IRepository<ProfileComment> _repository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateCommentCommandHandler(IRepository<ProfileComment> repository, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CustomResult<string>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

            //if (userIdClaim == null)
            //{
            //    var error = "User ID claim Alanı Tokende Bulunamadı Unauthorized.";
            //    return new CustomResult<string>(null, HttpStatusCode.Unauthorized, new List<string> { error });
            //}

            var followerUser = await _userManager.FindByIdAsync(request.AppUserID.ToString());



            var comment = new ProfileComment
            {
                AppUserID = request.AppUserID,
                WriterID = int.Parse(userIdClaim.Value),
                View = 0,
                Hidden = true,
                Comment = request.Comment,
                Like = 0,
                Time = DateTime.Now,
            };
            if(comment.AppUserID != comment.WriterID)
            {
                await _repository.CreateAsync(comment);

                return new CustomResult<string>("Yorum Başarıyla Eklendi.", HttpStatusCode.OK);
            }

            return new CustomResult<string>("Kendine Yorum Yapamazsın.", HttpStatusCode.BadRequest);
        }
    }
}

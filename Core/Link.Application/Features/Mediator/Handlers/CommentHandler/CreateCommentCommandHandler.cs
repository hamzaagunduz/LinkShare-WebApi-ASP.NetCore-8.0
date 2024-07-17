using FluentValidation;
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
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand,AppResponse>
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

        public async Task<AppResponse> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
             var userIdClaim = _httpContextAccessor.HttpContext.User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

                if (userIdClaim == null)
                {
                    throw new UnauthorizedAccessException("User ID claim not found in token.");
                }

                var followerUser = await _userManager.FindByIdAsync(request.AppUserID.ToString());

                var comment = new ProfileComment
                {
                    AppUserID = request.AppUserID,
                    WriterID = int.Parse(userIdClaim.Value),
                    View = request.View,
                    Hidden = request.Hidden,
                    Comment = request.Comment,
                    Like = request.Like,
                    Time = DateTime.Now,
                };



                await _repository.CreateAsync(comment);
                return await Task.FromResult<AppResponse>(new SuccessResponse(ResultMessages.CREATED_NOTE_SUCCESSFULLY));

        }







    }
    }

﻿using Link.Application.Common;
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

            if (userIdClaim == null)
            {
                return new CustomResult<string>("User ID claim not found in token.", HttpStatusCode.Unauthorized);
            }

            var followerUser = await _userManager.FindByIdAsync(request.AppUserID.ToString());

            if (followerUser == null)
            {
                return new CustomResult<string>("App User not found.", HttpStatusCode.NotFound);
            }

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

            return new CustomResult<string>("Comment created successfully.", HttpStatusCode.OK);
        }
    }
}

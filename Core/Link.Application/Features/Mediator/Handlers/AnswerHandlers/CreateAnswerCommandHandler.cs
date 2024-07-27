using Link.Application.Common;
using Link.Application.Features.Mediator.Commands.AnswerCommands;
using Link.Application.Features.Mediator.Commands.Comment;
using Link.Application.Interfaces;
using Link.Application.Interfaces.CommentRepository;
using Link.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Handlers.AnswerHandlers
{
    public class CreateAnswerCommandHandler : IRequestHandler<CreateAnswerCommand, CustomResult<string>>
    {
        private readonly IRepository<Answer> _repository;
        private readonly IRepository<ProfileComment> _repositoryComment;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateAnswerCommandHandler(IRepository<Answer> repository, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor, IRepository<ProfileComment> repositoryComment)
        {
            _repository = repository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _repositoryComment = repositoryComment;
        }
        public async Task<CustomResult<string>> Handle(CreateAnswerCommand request, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

            var answer = new Answer
            {
                AppUserID= int.Parse(userIdClaim.Value),
                AnswerText = request.AnswerText,
                ProfileCommentID = request.ProfileCommentID,
                Time = DateTime.Now,
                View = 0,
                LikeCount = 0
            };

            await _repository.CreateAsync(answer);

            var comment = await _repositoryComment.GetByIdAsync(request.ProfileCommentID);
            if (comment != null)
            {
                comment.Hidden = false;
                await _repositoryComment.UpdateAsync(comment);
            }
            return new CustomResult<string>("Yorum Başarıyla Eklendi.", HttpStatusCode.OK);


        }
    }
}

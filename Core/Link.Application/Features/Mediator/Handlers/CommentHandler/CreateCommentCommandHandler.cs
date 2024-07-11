using Link.Application.Features.Mediator.Commands.Comment;
using Link.Application.Interfaces;
using Link.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Handlers.CommentHandler
{
    public class CreateCommentCommandHandler: IRequestHandler<CreateCommentCommand>
    {
        private readonly IRepository<ProfileComment> _repository;
        private readonly UserManager<AppUser> _userManager;

        public CreateCommentCommandHandler(IRepository<ProfileComment> repositor, UserManager<AppUser> userManager) 
        {
            _repository = repositor;
            _userManager = userManager;
        }

        public async Task Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var followerUser = await _userManager.FindByIdAsync(request.WriterID.ToString());

            var following = new ProfileComment
            {
                AppUserID = request.AppUserID,
                WriterID = request.WriterID,
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

using Link.Application.Features.Mediator.Results.CommentResults;
using Link.Application.Interfaces.CommentRepository;
using Link.Domain.Entities;
using Link.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Persistence.Repository.CommentRepositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly LinkContext _context;

        public CommentRepository(LinkContext context)
        {
            _context = context;
        }

        public async Task<List<ProfileComment>> GetCommentsByAppUserIDAsync(int appUserID)
        {
            return await _context.ProfileComments
                                 .Where(c => c.AppUserID == appUserID)
                                 .ToListAsync();

        }

        public async Task<List<GetCommentsWithAppUserQueryResult>> GetCommentsWithAppUserByUserIDAsync(int appUserID)
        {
            var commentsWithWriters = await (from comment in _context.ProfileComments
                                             join user in _context.AppUsers on comment.WriterID equals user.Id
                                             where comment.AppUserID == appUserID
                                             select new GetCommentsWithAppUserQueryResult
                                             {
                                                 View = comment.View,
                                                 Like = comment.Like,
                                                 FirstName = user.FirstName,
                                                 SurName = user.SurName,
                                                 UserName = user.UserName,
                                                 Comment=comment.Comment
                                             }).ToListAsync();

            return commentsWithWriters;
        }


    }
}
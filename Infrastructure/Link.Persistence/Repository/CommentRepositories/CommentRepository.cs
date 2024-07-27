using Link.Application.Features.Mediator.Results.CommentResults;
using Link.Application.Interfaces.CommentRepository;
using Link.Domain.Entities;
using Link.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
                                             orderby comment.Time descending // CreatedDate'e göre azalan sırada sırala
                                             select new GetCommentsWithAppUserQueryResult
                                             {
                                                 View = comment.View,
                                                 Like = comment.Like,
                                                 FirstName = user.FirstName,
                                                 SurName = user.SurName,
                                                 UserName = user.UserName,
                                                 Comment = comment.Comment,
                                                 ProfileCommentID = comment.ProfileCommentID,
                                             }).ToListAsync();

            return commentsWithWriters;
        }



        public async Task<List<GetAnswersForCommentQueryResult>> GetAnswersForCommentByIdAsync(int profileCommentID)
        {
            var answersWithUsers = await (from answer in _context.Answers
                                          join user in _context.AppUsers on answer.AppUserID equals user.Id
                                          where answer.ProfileCommentID == profileCommentID
                                          select new GetAnswersForCommentQueryResult
                                          {
                                              AnswerID = answer.AnswerID,
                                              AnswerText = answer.AnswerText,
                                              View=answer.View,
                                              LikeCount = answer.LikeCount,
                                              Time=answer.Time,
                                              AppUserID=answer.AppUserID,
                                              UserName = user.UserName,
                                              FirstName = user.FirstName,
                                              SurName = user.SurName
                                          }).ToListAsync();

            return answersWithUsers;
        }
        public async Task<ProfileComment> GetCommentByProfileCommentIDAsync(int profileCommentID)
        {
            return await _context.ProfileComments
                                 .FirstOrDefaultAsync(c => c.ProfileCommentID == profileCommentID);
        }

        //public async Task<List<Answer>> GetAnswersForCommentByIdAsync(int profileCommentID)
        //{
        //    return await _context.Answers
        //                         .Where(a => a.ProfileCommentID == profileCommentID)
        //                         .ToListAsync();
        //}

    }
}
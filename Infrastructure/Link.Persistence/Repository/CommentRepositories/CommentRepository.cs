using Link.Application.Features.Mediator.Results.CommentResults;
using Link.Application.Interfaces.CommentRepository;
using Link.Domain.Entities;
using Link.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Link.Application.Features.Mediator.Commands.LikeCommands;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityType = Link.Application.Features.Mediator.Commands.LikeCommands.EntityType;

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
                                                 AppUserID=comment.AppUserID,
                                                 Hidden=comment.Hidden
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


        public async Task<List<GetCommentAndAnwerQueryResult>> GetProfileCommentsWithAnswersAsync(int page, int pageSize)
        {
            var commentsWithAnswers = await (from comment in _context.ProfileComments
                                             join answer in _context.AppUsers on comment.WriterID equals answer.Id
                                             join writer in _context.Answers on comment.ProfileCommentID equals writer.ProfileCommentID into answers
                                             from writer in answers.DefaultIfEmpty()
                                             join answerUser in _context.AppUsers on writer.AppUserID equals answerUser.Id into answerUsers
                                             from answerUser in answerUsers.DefaultIfEmpty()
                                             where comment.Hidden == false // Yalnızca yanıtı olan yorumları getir
                                             orderby comment.Time descending
                                             select new GetCommentAndAnwerQueryResult
                                             {
                                                 ProfileCommentID = comment.ProfileCommentID,
                                                 Comment = comment.Comment,
                                                 View = comment.View,
                                                 Like = comment.Like,
                                                 WriterID = comment.WriterID,
                                                 Hidden = comment.Hidden,
                                                 Time = comment.Time,
                                                 AppUserID = comment.AppUserID,
                                                 // Writer bilgileri, yorumun yazarı için
                                                 WriterFirstName = answer.FirstName,
                                                 WriterSurName = answer.SurName,
                                                 WriterUserName = answer.UserName,
                                                 // Yanıt bilgileri, yanıtın yazarı için
                                                 AnswerID = writer.AnswerID,
                                                 AnswerText = writer.AnswerText,
                                                 AnserView = writer.View,
                                                 LikeCount = writer.LikeCount,
                                                 AnswerTime = writer.Time,
                                                 FirstName = answerUser.FirstName,
                                                 SurName = answerUser.SurName,
                                                 UserName = answerUser.UserName
                                             }).Skip((page - 1) * pageSize) // Sayfa başına atlama işlemi
                                                 .Take(pageSize) // Sayfa boyutuna göre al
                                                 .ToListAsync();

            return commentsWithAnswers;
        }


        public async Task<List<GetLikersQueryResult>> GetLikersForCommentAsync(int profileCommentID)
        {
            var likers = await (from like in _context.Like
                                join user in _context.AppUsers on like.AppUserID equals user.Id
                                where like.ProfileCommentID == profileCommentID
                                select new GetLikersQueryResult
                                {
                                    UserID = user.Id,
                                    UserName = user.UserName,
                                    FirstName = user.FirstName,
                                    SurName = user.SurName
                                }).ToListAsync();

            return likers;
        }

        public async Task<List<GetLikersQueryResult>> GetLikersForAnswerAsync(int answerID)
        {
            var likers = await (from like in _context.Like
                                join user in _context.AppUsers on like.AppUserID equals user.Id
                                where like.AnswerID == answerID
                                select new GetLikersQueryResult
                                {
                                    UserID = user.Id,
                                    UserName = user.UserName,
                                    FirstName = user.FirstName,
                                    SurName = user.SurName
                                }).ToListAsync();

            return likers;
        }

        public async Task<List<GetLikersQueryResult>> GetLikersAsync(int entityId, Application.Features.Mediator.Commands.LikeCommands.EntityType entityType, int page, int pageSize)
        {
            var query = from like in _context.Like
                        join user in _context.AppUsers on like.AppUserID equals user.Id
                        where (entityType == EntityType.Answer && like.AnswerID == entityId) ||
                              (entityType == EntityType.Comment && like.ProfileCommentID == entityId)
                        select new GetLikersQueryResult
                        {
                            UserID = user.Id,
                            UserName = user.UserName,
                            FirstName = user.FirstName,
                            SurName = user.SurName
                        };

            var pagedLikers = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return pagedLikers;
        }


        public async Task<List<GetCommentAndAnwerQueryResult>> GetTopLikedCommentsWithAnswersAsync(int topCount)
        {
            var oneWeekAgo = DateTime.UtcNow.AddDays(-7);

            var commentsWithAnswers = await (from comment in _context.ProfileComments
                                             join answer in _context.Answers on comment.ProfileCommentID equals answer.ProfileCommentID into answers
                                             from answer in answers.DefaultIfEmpty()
                                             join answerUser in _context.AppUsers on answer.AppUserID equals answerUser.Id into answerUsers
                                             from answerUser in answerUsers.DefaultIfEmpty()
                                             join writer in _context.AppUsers on comment.WriterID equals writer.Id
                                             where comment.Time >= oneWeekAgo // Son bir hafta içindeki yorumları getir
                                             orderby comment.Like descending // Beğeniye göre sırala
                                             select new GetCommentAndAnwerQueryResult
                                             {
                                                 ProfileCommentID = comment.ProfileCommentID,
                                                 Comment = comment.Comment,
                                                 View = comment.View,
                                                 Like = comment.Like,
                                                 WriterID = comment.WriterID,
                                                 Hidden = comment.Hidden,
                                                 Time = comment.Time,
                                                 AppUserID = comment.AppUserID,
                                                 WriterFirstName = writer.FirstName,
                                                 WriterSurName = writer.SurName,
                                                 WriterUserName = writer.UserName,
                                                 AnswerID = answer.AnswerID,
                                                 AnswerText = answer.AnswerText,
                                                 AnserView = answer.View,
                                                 LikeCount = answer.LikeCount,
                                                 AnswerTime = answer.Time,
                                                 FirstName = answerUser.FirstName,
                                                 SurName = answerUser.SurName,
                                                 UserName = answerUser.UserName
                                             })
                                             .Take(topCount) // En yüksek beğeniyi alan yorumları al
                                             .ToListAsync();

            return commentsWithAnswers;
        }


    }
}
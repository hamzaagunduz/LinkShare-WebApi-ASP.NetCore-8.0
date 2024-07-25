using Link.Application.Features.Mediator.Results.CommentResults;
using Link.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Interfaces.CommentRepository
{
    public interface ICommentRepository
    {
        Task<List<ProfileComment>> GetCommentsByAppUserIDAsync(int appUserID);
        Task<List<GetCommentsWithAppUserQueryResult>> GetCommentsWithAppUserByUserIDAsync(int appUserID);

    }
}

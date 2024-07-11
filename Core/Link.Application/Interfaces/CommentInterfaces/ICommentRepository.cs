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
    }
}

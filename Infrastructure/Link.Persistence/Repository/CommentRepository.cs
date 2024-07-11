using Link.Application.Interfaces.CommentRepository;
using Link.Domain.Entities;
using Link.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Persistence.Repository
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
    }
}
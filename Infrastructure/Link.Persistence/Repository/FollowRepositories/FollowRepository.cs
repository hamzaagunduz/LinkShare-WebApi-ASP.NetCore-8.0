using Link.Application.Interfaces.FollowInterfaces;
using Link.Domain.Entities;
using Link.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Persistence.Repository.FollowRepositories
{
    public class FollowRepository : IFollowRepository
    {
        private readonly LinkContext _context;
        public FollowRepository(LinkContext context)
        {
            _context = context;
        }
        public async Task<List<Follower>> GetFollowersAsync(int userId)
        {
            return await _context.Set<Follower>()
                          .Where(f => f.AppUserID == userId)
                          .ToListAsync();
        }

        public async Task<List<Following>> GetFollowingAsync(int userId)
        {
            return await _context.Set<Following>()
                .Where(f => f.AppUserID == userId)
                .ToListAsync();
        }
    }
}

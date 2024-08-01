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


        public async Task<List<AppUser>> GetRandomUsers(int count)
        {
            Random random = new Random();
            int totalUserCount =  _context.Users.Count();

            // Toplam kullanıcı sayısı belirtilen sayıdan az ise, tüm kullanıcıları getir
            if (totalUserCount <= count)
            {
                return _context.Users.ToList();
            }
            else
            {
                // Tüm kullanıcıların ID'lerini al
                var allUserIds = _context.Users.Select(u => u.Id).ToList();

                // Rastgele kullanıcı ID'leri seç
                HashSet<int> selectedIds = new HashSet<int>();
                while (selectedIds.Count < count)
                {
                    int randomIdIndex = random.Next(allUserIds.Count);
                    int randomId = allUserIds[randomIdIndex];
                    if (!selectedIds.Contains(randomId))
                    {
                        selectedIds.Add(randomId);
                    }
                }

                // Seçilen ID'lere göre kullanıcıları getir
                var selectedUsers = _context.Users
                    .Where(u => selectedIds.Contains(u.Id))
                    .ToList();

                return selectedUsers;
            }
        }

    }
}

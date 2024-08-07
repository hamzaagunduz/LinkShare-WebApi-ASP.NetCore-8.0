using Link.Application.Helper;
using Link.Application.Interfaces.UserInterfaces;
using Link.Domain.Entities;
using Link.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Persistence.Repository.UserRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LinkContext _context;
        public UserRepository(LinkContext context)
        {
            _context = context;
        }

        public async Task<List<AppUser>> SearchUsersAsync(string query)
        {
            var users = await _context.AppUsers.ToListAsync();

            // Merge Sort ile sıralama
            var sortedUsers = MergeSortHelper.MergeSort(users, user => user.FirstName);

            // Binary Search kullanarak arama yapabiliriz (sıralı listede arama)
            return sortedUsers.Where(user =>
                user.FirstName.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                user.SurName.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}

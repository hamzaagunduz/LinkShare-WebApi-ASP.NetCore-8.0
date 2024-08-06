using Link.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Interfaces.FollowInterfaces
{
    public interface IFollowRepository
    {
        Task<List<Follower>> GetFollowersAsync(int userId);
        Task<List<Following>> GetFollowingAsync(int userId);
        Task<List<AppUser>> GetRandomUsers(int count);
    }
}

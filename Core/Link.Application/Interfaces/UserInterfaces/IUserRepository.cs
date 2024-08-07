using Link.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Interfaces.UserInterfaces
{
    public interface  IUserRepository
    {
        Task<List<AppUser>> SearchUsersAsync(string query);
    }
}

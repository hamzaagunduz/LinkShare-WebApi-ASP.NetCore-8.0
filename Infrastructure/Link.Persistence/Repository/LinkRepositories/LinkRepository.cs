using Link.Application.Interfaces.LinkInterfaces;
using Link.Domain.Entities;
using Link.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Persistence.Repository.LinkRepositories
{
    public class LinkRepository : ILinkRepository
    {
        private readonly LinkContext _context;
        public LinkRepository(LinkContext context)
        {
            _context = context;
        }
        public List<Linke> GetByIdUserLink(int userId)
        {
            return _context.Set<Linke>().Where(link => link.AppUserID == userId).ToList();
        }
    }
}

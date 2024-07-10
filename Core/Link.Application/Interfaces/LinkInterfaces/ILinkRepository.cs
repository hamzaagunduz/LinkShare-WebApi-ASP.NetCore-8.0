using Link.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Interfaces.LinkInterfaces
{
    public interface ILinkRepository
    {
        public List<Linke> GetByIdUserLink(int userId);
    }
}

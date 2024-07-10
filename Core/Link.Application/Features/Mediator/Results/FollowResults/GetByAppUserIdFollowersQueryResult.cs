using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Results.FollowResults
{
    public class GetByAppUserIdFollowersQueryResult
    {
        public int FollowerID { get; set; }
        public int AppUserFollowerID { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public int AppUserID { get; set; }
    }
}

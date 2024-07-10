using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Domain.Entities
{
    public class Following
    {
        public int FollowingID { get; set; }
        public int AppUserFollowingID { get; set; }
        public int AppUserID { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public AppUser AppUser { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Domain.Entities
{
    public class Profile
    {
        public int ProfileID { get; set; }
        public int AppUserID { get; set; }
        public AppUser AppUser { get; set; }
        public List<ProfileComment> ProfileComments { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Domain.Entities
{
    public class Linke
    {
        public int LinkeID { get; set; }
        public string LinkName { get; set; }
        public string LinkUrl{ get; set; }
        public int AppUserID { get; set; }
        public AppUser AppUser { get; set; }
    }
}

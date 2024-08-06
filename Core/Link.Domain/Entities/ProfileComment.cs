using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Domain.Entities
{
    public class ProfileComment
    {
        public int ProfileCommentID { get; set; }
        public string Comment { get; set; }
        public int View { get; set; }
        public int Like { get; set; }
        public int WriterID { get; set; }
        public bool Hidden { get; set; }
        public DateTime Time { get; set; }

        public int AppUserID { get; set; }
        public AppUser AppUser { get; set; }

        public Answer? Answers { get; set; }

        public List<Like> Likes { get; set; } = new List<Like>();
    }




}

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
        public string View { get; set; }
        public string Like { get; set; }
        public bool Hidden { get; set; }
        public DateTime Time { get; set; }

        public int ProfileID { get; set; }
        public Profile Profiles { get; set; }

        public Answer? Answers { get; set; }

    }
}

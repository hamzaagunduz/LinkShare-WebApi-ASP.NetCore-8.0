using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Domain.Entities
{
    public class Like
    {
        public int LikeID { get; set; }
        public int AppUserID { get; set; }
        public AppUser AppUser { get; set; }

        public int? ProfileCommentID { get; set; }
        public ProfileComment? ProfileComment { get; set; }

        public int? AnswerID { get; set; }
        public Answer? Answer { get; set; }

        public DateTime Time { get; set; }
    }
}

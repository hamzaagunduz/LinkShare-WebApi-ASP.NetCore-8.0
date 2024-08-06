using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Domain.Entities
{
    public class Answer
    {
        public int AnswerID { get; set; }
        public int AppUserID { get; set; }
        public string AnswerText { get; set; }
        public int View { get; set; }
        public int LikeCount { get; set; }
        public DateTime Time { get; set; }
        public int? ProfileCommentID { get; set; }
        public ProfileComment? ProfileComment { get; set; }

        public List<Like> Likes { get; set; } = new List<Like>();
    }
}

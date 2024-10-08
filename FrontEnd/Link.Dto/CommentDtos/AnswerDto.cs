﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Dto.CommentDtos
{
    public class AnswerDto
    {
        public int AnswerID { get; set; }
        public string AnswerText { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string UserName { get; set; }
        public int View { get; set; }
        public int LikeCount { get; set; }
        public DateTime Time { get; set; }
        public int AppUserID { get; set; }
        public int ProfileCommentID { get; set; } // Bu özelliği ekleyin
    }
}

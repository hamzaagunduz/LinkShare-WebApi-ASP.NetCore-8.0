using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Results.CommentResults
{
    public class GetCommentAndAnwerQueryResult
    {
        public int ProfileCommentID { get; set; }
        public int WriterID { get; set; }

        public string? WriterFirstName { get; set; }
        public string? WriterSurName { get; set; }
        public string? WriterUserName { get; set; }
        public string Comment { get; set; }
        public int View { get; set; }
        public int Like { get; set; }
        public bool Hidden { get; set; }
        public DateTime Time { get; set; }
        public int? AppUserID { get; set; }

        public string? FirstName { get; set; }
        public string? SurName { get; set; }
        public string? UserName { get; set; }


        public int? AnswerID { get; set; }
        public string? AnswerText { get; set; }
        public int ?AnserView { get; set; }
        public int? LikeCount { get; set; }
        public DateTime? AnswerTime { get; set; }
    }
}

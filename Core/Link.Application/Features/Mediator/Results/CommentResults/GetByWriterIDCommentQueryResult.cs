using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Results.CommentResults
{
    public class GetByWriterIDCommentQueryResult
    {
        public int AppUserID { get; set; }
        public string Comment { get; set; }
        public int View { get; set; }
        public int Like { get; set; }
        public bool Hidden { get; set; }
        public DateTime Time { get; set; }
    }
}

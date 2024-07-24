using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Dto.CommentDtos
{
    public class AddCommentDto
    {
        public int AppUserID { get; set; }
        public string Comment { get; set; }
        public int View { get; set; }
        public int Like { get; set; }
        public bool Hidden { get; set; }
        public DateTime Time { get; set; }
    }
}

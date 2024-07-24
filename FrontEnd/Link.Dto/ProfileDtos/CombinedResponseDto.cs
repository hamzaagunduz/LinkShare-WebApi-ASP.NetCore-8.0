using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Dto.ProfileDtos
{
    public class CombinedResponseDto
    {
        public List<GetLinkDto> Links { get; set; }
        public List<CommentDto> Comments { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Link.Dto.CommentDtos;
using Link.Dto.LinkDto;
using Link.Dto.ProfileDtos;

namespace Link.Dto.ApiResponseDtos
{
    public class CombinedResponseDto
    {
        public List<GetLinkDto> Links { get; set; }
        public List<CommentDto> Comments { get; set; }
        public GetAppUserDto GetAppUserDto { get; set; }
        public Dictionary<int, List<AnswerDto>> CommentAnswers { get; set; }

    }

}

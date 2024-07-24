﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Dto.ProfileDtos
{
    public class CommentDto
    {


            public int WriterID { get; set; }
            public string Comment { get; set; }
            public int View { get; set; }
            public int Like { get; set; }
            public bool Hidden { get; set; }
            public DateTime Time { get; set; }

    }
}

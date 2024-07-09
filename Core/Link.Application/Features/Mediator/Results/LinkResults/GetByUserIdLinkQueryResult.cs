using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Results.LinkResults
{
    public class GetByUserIdLinkQueryResult
    {
        public int LinkeID { get; set; }
        public string LinkName { get; set; }
        public string LinkUrl { get; set; }
    }
}

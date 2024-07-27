using AutoMapper;
using Link.Application.Features.Mediator.Commands.LinkCommands;
using Link.Application.Features.Mediator.Results.LinkResults;
using Link.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.AutoMapper.Link
{
    public class LinkProfile : Profile
    {
        public LinkProfile()
        {
            CreateMap<Linke, CreateLinkCommand>().ReverseMap();
            CreateMap<Linke, GetByUserIdLinkQueryResult>().ReverseMap();
        }
    }

}

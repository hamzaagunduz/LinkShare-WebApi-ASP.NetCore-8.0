using Link.Dto.ApiResponseDtos;
using Link.Dto.ProfileDtos;
using Link.WebUI.ViewComponents.ProfileViewComponents;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace Link.WebUI.ViewComponents.ProfileViewComponents
{
    public class _ProfileCommentComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }


    }
}





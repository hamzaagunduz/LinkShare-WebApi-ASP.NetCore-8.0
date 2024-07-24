using Link.Dto.ApiResponseDtos;
using Link.Dto.ProfileDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Link.WebUI.ViewComponents.ProfileViewComponents
{
    public class _ProfileLinkComponentPartial : ViewComponent
    {



        public IViewComponentResult Invoke()
        {
            return View();
        }




    }
}

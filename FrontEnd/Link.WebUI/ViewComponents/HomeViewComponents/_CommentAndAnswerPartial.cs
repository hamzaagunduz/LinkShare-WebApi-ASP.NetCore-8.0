using Microsoft.AspNetCore.Mvc;

namespace Link.WebUI.ViewComponents.HomeViewComponents
{
    public class _CommentAndAnswerPartial : ViewComponent
    {



        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}

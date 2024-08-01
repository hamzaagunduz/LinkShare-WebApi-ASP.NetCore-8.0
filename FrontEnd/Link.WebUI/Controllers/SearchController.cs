using Microsoft.AspNetCore.Mvc;

namespace Link.WebUI.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

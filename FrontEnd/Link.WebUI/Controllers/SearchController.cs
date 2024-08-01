using Microsoft.AspNetCore.Mvc;

namespace Link.WebUI.Controllers
{
    public class SearchController : Controller
    {
        [HttpGet("Search")]
        public IActionResult Index()
        {
            return View();
        }
    }
}

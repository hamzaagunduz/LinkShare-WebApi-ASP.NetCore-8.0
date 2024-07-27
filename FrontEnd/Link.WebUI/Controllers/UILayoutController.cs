using Microsoft.AspNetCore.Mvc;

namespace Link.WebUI.Controllers
{
    public class UILayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

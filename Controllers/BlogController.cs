using Microsoft.AspNetCore.Mvc;

namespace Jurr.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

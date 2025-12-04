using Microsoft.AspNetCore.Mvc;

namespace Jurr.Controllers
{
    public class ServicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

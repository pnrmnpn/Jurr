using Microsoft.AspNetCore.Mvc;

namespace Jurr.Controllers
{
    public class TeamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

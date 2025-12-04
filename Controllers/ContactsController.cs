using Microsoft.AspNetCore.Mvc;

namespace Jurr.Controllers
{
    public class ContactsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

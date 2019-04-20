using Microsoft.AspNetCore.Mvc;

namespace Miunie.Server.Controllers
{
    public class Bot : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}

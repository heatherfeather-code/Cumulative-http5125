using Microsoft.AspNetCore.Mvc;

namespace Cumulative1.Controllers
{
    public class StudentPageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

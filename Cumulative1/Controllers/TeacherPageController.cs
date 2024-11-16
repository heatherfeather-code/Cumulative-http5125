using Microsoft.AspNetCore.Mvc;

namespace Cumulative1.Controllers
{
    public class TeacherPageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

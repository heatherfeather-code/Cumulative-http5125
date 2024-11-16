using Microsoft.AspNetCore.Mvc;

namespace Cumulative1.Controllers
{
    public class CoursePageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

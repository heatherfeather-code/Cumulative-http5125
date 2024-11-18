using Microsoft.AspNetCore.Mvc;
using Cumulative1.Models;

namespace Cumulative1.Controllers
{
    public class CoursePageController : Controller
    {
        private readonly CourseAPIController _api;

        public CoursePageController(CourseAPIController api)
        {
            _api = api;
        }

        //GET: CoursePage/ List
        public IActionResult ListCourse()
        {
            List <Course> Course = _api.ListCourses();
            return View(Course);
        }

        //GET: CoursePage/Show/ {id}

        public IActionResult ShowCourse(int id)
        {
            Course SingleCourse = _api.SingleCourse(id);
            return View(SingleCourse);
        }

    }
}

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
        public IActionResult ListCourses()
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

        //UPDATE COURSE FUNCTIONALITY

        //GET: CoursePage
        [HttpGet]
        public IActionResult EditCourse(int id)
        {
            Course SingleCourse = _api.SingleCourse(id);
            return View(SingleCourse);  

        }

        //POST: CoursePage/UpdateCourse/{id}

        [HttpPost]
        public IActionResult UpdateCourse(Course courseToUpdate)
        {
            //just need to add the parameters for this function
            int numRowsAffected = _api.UpdateCourse(courseToUpdate.CourseCode,courseToUpdate.TeacherId, courseToUpdate.CourseStartDate,courseToUpdate.CourseEndDate,courseToUpdate.CourseName,courseToUpdate.CourseId);
            if (numRowsAffected == 0)
            {
                return RedirectToAction("ListCourse");
            }
            return RedirectToAction("ShowCourse", new {id = courseToUpdate.CourseId});
        }


    }
}

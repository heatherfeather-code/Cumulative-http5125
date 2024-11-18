using Microsoft.AspNetCore.Mvc;
using Cumulative1.Models;

namespace Cumulative1.Controllers
{
    public class TeacherPageController : Controller
    {
        private readonly TeacherAPIController _api;
        public TeacherPageController(TeacherAPIController api)
        {
            _api = api;
        }
        //GET: TeacherPage/List
        public IActionResult ListTeacher()
        {
            List<Teacher> Teachers = _api.ListTeachers();
            return View(Teachers);
        }

        //GET: TeacherPage/Show/{id}
        public IActionResult ShowTeacher(int id)
        {
            Teacher SingleTeacher = _api.SingleTeacher(id);
            return View(SingleTeacher);
        }
    }
}

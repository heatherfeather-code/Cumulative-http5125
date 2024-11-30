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
        //GET: TeacherPage/New
        [HttpGet]
        public IActionResult AddTeacher(int id)
        {
            return View();
        }

        //POST: TeacherPage/AddTeacher
        [HttpPost]
        public IActionResult Create(Teacher AddTeacher)
        {
            int TeacherId = _api.AddTeacher (AddTeacher.TeacherFName,AddTeacher.TeacherLName,AddTeacher.EmployeeNumber,AddTeacher.TeacherHireDate, AddTeacher.TeacherSalary);
            
            if (TeacherId == 0) 
            {
                return RedirectToAction("ListTeacher");
            }
            
            return RedirectToAction("ShowTeacher", new {id = TeacherId });
        }

        //GET: TeacherPage/DeleteConfirm/{id}
        [HttpGet]
        public IActionResult DeleteConfirm(int id)
        {
            Teacher SelectedTeacher = _api.SingleTeacher(id);
            return View(SelectedTeacher); 
        }
        //POST: TeacherPage/Delete/{id}
        [HttpPost]
        public IActionResult Delete(int id)
        {
            int TeacherId = _api.DeleteTeacher(id);

            //redirects to list action

            return RedirectToAction("ListTeacher");
        }
    }
} 

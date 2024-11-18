using Microsoft.AspNetCore.Mvc;
using Cumulative1.Models;
///<summary>
///This controller is to display the page within a browser, it allows front end functionality. 
/// </summary>
namespace Cumulative1.Controllers
{
    public class StudentPageController : Controller
    {
        private readonly StudentAPIController _api;

        public StudentPageController(StudentAPIController api)
        {
            _api = api;
        }
        //GET: StudentPage/ListStudent
        public IActionResult ListStudent()
        {
            List<Student> Students = _api.ListStudents();
            return View(Students);
        }

        //GET: StudentPage/Show/ {id}
        public IActionResult ShowStudent(int id)
        {
            Student SingleStudent = _api.SingleStudent(id);
            return View(SingleStudent);
        }

    }
}

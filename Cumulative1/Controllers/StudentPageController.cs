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

        //UPDATE STUDENT FUNCTIONALITY

        //GET: StudentPage/EditStudent/{id}
        [HttpGet]
        public IActionResult EditStudent(int id) 
        {
            Student SelectedStudent = _api.SingleStudent(id);
            return View(SelectedStudent);
        }

        //POST: StudentPage/UpdateStudent/{id}
        [HttpPost]
        public IActionResult UpdateStudent(Student studentToUpdate)
        {
            int numRowsAffected = _api.UpdateStudent(studentToUpdate.StudentId, studentToUpdate.StudentFName, studentToUpdate.StudentLName,studentToUpdate.StudentNumber, studentToUpdate.StudentEnrolDate);

            if (numRowsAffected == 0) 
            {
                return RedirectToAction("ListStudent");
            }
            return RedirectToAction("ShowStudent", new { id = studentToUpdate.StudentId });
        }

        //Initative from C2: Add student functionality**

        //Add Student Functionality

        //GET:StudentPage/New
        [HttpGet]
        public IActionResult AddStudent(int id)
        {
            return View();
        }

        //POST: StudentPage/AddStudent
        [HttpPost]
        public IActionResult Create(Student AddStudent)
        {
            int StudentId = _api.AddStudent(AddStudent.StudentFName, AddStudent.StudentLName, AddStudent.StudentNumber, AddStudent.StudentEnrolDate,AddStudent.StudentId);
            if (StudentId == 0)
            {
                return RedirectToAction("ListStudent");
            }
            return RedirectToAction("ShowStudent", new { id = StudentId });

        }
    }
}

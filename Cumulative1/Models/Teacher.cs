/// <summary>
/// Teacher class: Allows access of the properties
/// from the teachers table
/// (TeacherId, TeacherFName, TeacherLName, TeacherHireDate, EmployeeNumber, TeacherSalary)
/// Allows the ability to access the properties for the other functionality as well, ie. Update and Delete. 
/// </summary>

namespace Cumulative1.Models
{
    public class Teacher 
    {
        public int TeacherId { get; set; }
        public string TeacherFName { get; set; }
        public string TeacherLName { get; set; }
        public DateTime TeacherHireDate { get; set; }
        public string EmployeeNumber { get; set; }
        public float TeacherSalary { get; set; }
       
    }
}

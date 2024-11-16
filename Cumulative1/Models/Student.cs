/// <summary>
/// Student Class: to be able to define the properties per student
/// class allows access to the properties (StudentId,
/// StudentFName, StudentLName, StudentNumber, and StudentEnrolDate)
/// </summary>


namespace Cumulative1.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentFName { get; set; }
        public string StudentLName { get; set; }
        public string StudentNumber { get; set; }
        public DateOnly StudentEnrolDate { get; set; }
    }
}

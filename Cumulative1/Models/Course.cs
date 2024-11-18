///<summary>
///Course Class: Allows access of properties from the Courses table
///(CourseId, CourseCode, CourseStartDate, CourseEndDate, TeacherId, CourseName)
/// </summary>
namespace Cumulative1.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public DateTime CourseStartDate { get; set; }
        public DateTime CourseEndDate { get; set; }
        public int TeacherId { get; set; }
        public string CourseName { get; set; }
    }
}

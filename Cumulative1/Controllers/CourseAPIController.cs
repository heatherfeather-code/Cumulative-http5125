using Cumulative1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using MySql.Data.MySqlClient;

namespace Cumulative1.Controllers
{
    [Route("api/Course")]
    [ApiController]
    public class CourseAPIController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        public CourseAPIController(SchoolDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Display courses on a web page with MVC controller.
        /// </summary>
        /// <example>
        /// GET api/Course/ListCourses
        /// </example>
        /// <returns>
        /// all courses within the database
        /// </returns>
        [HttpGet]
        [Route(template: "ListCourses")]

        public List<Course> ListCourses()
        {
            //create empty list of courses
            List<Course> Courses = new List<Course>();
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();

                //estasblish a query for the database

                MySqlCommand Command = Connection.CreateCommand();

                //SQL Query for desired output

                Command.CommandText = "SELECT * FROM Courses";

                //put expected results of query into a variable:

                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    while (ResultSet.Read())
                    {
                        //Return all information on all courses
                        int Id = Convert.ToInt32(ResultSet["courseid"]);
                        string CourseCode = ResultSet["coursecode"].ToString();
                        int ID = Convert.ToInt32(ResultSet["teacherid"]);
                        DateTime StartDate = Convert.ToDateTime(ResultSet["startdate"]);
                        DateTime FinishDate = Convert.ToDateTime(ResultSet["finishdate"]);
                        string CourseName = ResultSet["coursename"].ToString();

                        Course CurrentCourse = new Course()
                        {
                            CourseId = Id,
                            CourseCode = CourseCode,
                            TeacherId = ID,
                            CourseStartDate = StartDate,
                            CourseEndDate = FinishDate,
                            CourseName = CourseName,


                        };
                        Courses.Add(CurrentCourse);

                    }

                    return Courses;
                }

            }
            //return null;
        }


        /// <summary>
        /// Accesses the database by index of course id
        /// </summary>
        /// <example>
        /// GET: CoursePage/ShowCourse/2
        /// GET: CoursePage/ShowCourse/5
        /// </example>
        /// <returns>
        /// returns the single course from the database based on indexing via course id. 
        /// </returns>
        [HttpGet]
        [Route(template: "SingleCourse")]
        public Course SingleCourse(int CourseId)
        {
            Course SingleCourse = null;

            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();

                //Establish a query for the database:
                MySqlCommand Command = Connection.CreateCommand();

                //SQL Query for expected results:
                Command.CommandText = "SELECT * FROM courses WHERE courseid = " + CourseId;

                //Put results of query into a variable

                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    while (ResultSet.Read())
                    {
                        int Id = Convert.ToInt32(ResultSet["courseid"]);
                        string CourseCode = ResultSet["coursecode"].ToString();
                        int ID = Convert.ToInt32(ResultSet["teacherid"]);
                        DateTime StartDate = Convert.ToDateTime(ResultSet["startdate"]);
                        DateTime FinishDate = Convert.ToDateTime(ResultSet["finishdate"]);
                        string CourseName = ResultSet["coursename"].ToString();

                        SingleCourse = new Course()
                        {
                            CourseId = Id,
                            CourseCode = CourseCode,
                            TeacherId = ID,
                            CourseStartDate = StartDate,
                            CourseEndDate = FinishDate,
                            CourseName = CourseName,
                        };

                    }

                    return SingleCourse;
                }
            }
                          
        }
    }
       
}

using Cumulative1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using MySql.Data.MySqlClient;

namespace Cumulative1.Controllers
{
    [Route("api/Student")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        public StudentAPIController(SchoolDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Controller function to access the database and  list all students 
        /// </summary>
        /// <example>
        /// GET api/Student/ ListStudents [{"studentId:1","studentFName":Jean, "studentLName": Smith,}]
        /// </example>
        /// <returns>
        /// compiled list of all students within the database
        /// </returns>
        [HttpGet]
        [Route(template: "ListStudents")]

        public List<Student> ListStudents()
        {
            List<Student> Students = new List<Student>();
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();
                MySqlCommand Command = Connection.CreateCommand();

                //SQL Query
                Command.CommandText = "SELECT * FROM students";

                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    while (ResultSet.Read())
                    {
                        // return all information on all students
                        int Id = Convert.ToInt32(ResultSet["studentid"]);
                        string FirstName = ResultSet["studentfname"].ToString();
                        string LastName = ResultSet["studentlname"].ToString();
                        string StudentNumber = ResultSet["studentnumber"].ToString();
                        DateTime StudentEnrolDate = Convert.ToDateTime(ResultSet["enroldate"]);

                        Student CurrentStudent = new Student()
                        {
                            StudentId = Id,
                            StudentFName = FirstName,
                            StudentLName = LastName,
                            StudentNumber = StudentNumber,
                            StudentEnrolDate = StudentEnrolDate,
                        };
                        Students.Add(CurrentStudent);
                    }
                    return Students;
                }

            }
        }

		/// <summary>
		/// Controller function to display the data of a single student
		/// 
		/// </summary>
		/// <example>
		/// GET api/Student/ShowStudent/23 {Elizabeth Thompson, N1736, 2018-08-07}
        /// GET api/Student/ShowStudent/ 1 {Sarah Valdez, N1678, 2018-06-18}
		/// </example>
		/// <returns>
		/// results of one student selected by id
		/// </returns>
		[HttpGet]
		[Route(template: "SingleStudent/{studentId}")]
		public Student SingleStudent(int studentId)
		{
			//create empty variable for single student
			Student SingleStudent = null;

			using (MySqlConnection Connection = _context.AccessDatabase())
			{
				Connection.Open();
				//Establish query for the database
				MySqlCommand Command = Connection.CreateCommand();

				//SQL Query for desired result
				Command.CommandText = "SELECT * FROM students WHERE studentid = " + studentId;

				//output the results of the query into a variable:

				using (MySqlDataReader ResultSet = Command.ExecuteReader())
				{
					while (ResultSet.Read())
					{
						int Id = Convert.ToInt32(ResultSet["studentid"]);
						string FirstName = ResultSet["studentfname"].ToString();
						string LastName = ResultSet["studentlname"].ToString();
						string StudentNumber = ResultSet["studentnumber"].ToString();
						DateTime StudentEnrolDate = Convert.ToDateTime(ResultSet["enroldate"]);

						SingleStudent = new Student()
						{
							StudentId = Id,
							StudentFName = FirstName,
							StudentLName = LastName,
							StudentNumber = StudentNumber,
							StudentEnrolDate = StudentEnrolDate,
						};
					}
					return SingleStudent;
				}
			}
		}

	}
}

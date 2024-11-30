using Cumulative1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using MySql.Data.MySqlClient;
using Mysqlx.Datatypes;


namespace Cumulative1.Controllers
{
    [Route("api/Teacher")]
    [ApiController]
    public class TeacherAPIController : ControllerBase
    {
        private readonly SchoolDbContext _context;
        public TeacherAPIController(SchoolDbContext context)
        {
            _context = context;
        }

		/// <summary>
		/// this controller will access connected database and list
		/// all teachers from the school database
		/// </summary>
		/// <exmaple>
		/// GET api/TeacherPage/ ListTeacher {"teacherId":1,"teacherFName": Aaron, "teacherLName": Smith, "Hiredate": 2020-4-6,"Salary": 60.65}
		/// GET api/TeacherPage/ListTeacher { 1, Jim Smith, 2009-07-08, 70.98}
		/// </exmaple>
		/// <returns></returns>
		[HttpGet]
        [Route(template: "ListTeachers")]
        public List<Teacher> ListTeachers()
        {
            //create an empty list of Teacher Names
            List<Teacher> Teachers = new List<Teacher>();
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();
                //establish a query for the database
                MySqlCommand Command = Connection.CreateCommand();

                //SQL Query
                Command.CommandText = "SELECT * FROM teachers";
                //put results of query into variable:

                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    while (ResultSet.Read())
                    {
                        //return all information on all teachers
                        int Id = Convert.ToInt32(ResultSet["teacherid"]);
                        string FirstName = ResultSet["teacherfname"].ToString();
                        string LastName = ResultSet["teacherlname"].ToString();
                        string EmployeeNumber = ResultSet["employeenumber"].ToString();
                        DateTime TeacherHireDate = Convert.ToDateTime(ResultSet["hiredate"]);
                        float Salary = Convert.ToSingle(ResultSet["salary"]);

                        Teacher CurrentTeacher = new Teacher()
                        {
                            TeacherId = Id,
                            TeacherFName = FirstName,
                            TeacherLName = LastName,
                            EmployeeNumber = EmployeeNumber,
                            TeacherHireDate = TeacherHireDate,
                            TeacherSalary = Salary,
                        };
                        Teachers.Add(CurrentTeacher);
                    }

					return Teachers;

				}

			}

        }
        /// <summary>
        /// Displays data of a single teacher within the database
        /// </summary>
        /// <example>
        /// GET: api/Teacher/ListTeacher/5 {Jessica Morris, 2012-06-4, T389,5}
        /// GET: api/Teacher/ListTeacher/ 14 {Billy Bob, 2011-1-30, T999, 20.00} 
        /// </example>
        /// <returns>
        /// single teacher with info: first and last name, hire date, employee id and id for database
        /// </returns>
        /// <route> api/teacher/singleteacher </route>
        [HttpGet]
        [Route(template: "SingleTeacher/{teacherId}")]
        public Teacher SingleTeacher(int teacherId)
        {
            //create an empty list of Teacher Names
            Teacher SingleTeacher = null;

            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();
                //establish a query for the database
                MySqlCommand Command = Connection.CreateCommand();

                //SQL Query:
                Command.CommandText = "SELECT * FROM teachers WHERE teacherid = " + teacherId;

                //put results of query into variable:

                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                   
                        while (ResultSet.Read())
                        {
                            //return all information on all teachers
                            int Id = Convert.ToInt32(ResultSet["teacherid"]);
                            string FirstName = ResultSet["teacherfname"].ToString();
                            string LastName = ResultSet["teacherlname"].ToString();
                            string EmployeeNumber = ResultSet["employeenumber"].ToString();
                            DateTime TeacherHireDate = Convert.ToDateTime(ResultSet["hiredate"]);
                            float Salary = Convert.ToSingle(ResultSet["salary"]);

                            SingleTeacher = new Teacher()
                            {
                                TeacherId = Id,
                                TeacherFName = FirstName,
                                TeacherLName = LastName,
                                EmployeeNumber = EmployeeNumber,
                                TeacherHireDate = TeacherHireDate,
                                TeacherSalary = Salary,
                            };
                        }


                        return SingleTeacher;

                    

                }

            }
        }
        /// <summary>
        /// Allows user to add a teacher, user can access the Add Teacher form via the List Teacher page
        /// </summary>
        /// <param name="teacherfname"></param>
        /// <param name="teacherlname"></param>
        /// <param name="employeenumber"></param>
        /// <param name="hiredate"></param>
        /// <param name="salary"></param>
        /// <example>
        /// POST: api/TeacherPage/ ListTeacher/AddTeacher {Christine Stewart, T654, 2018-04-20, 15.60}
        /// POST: api/TeacherPage/ListTeacher/AddTeacher {Samantha Lee, T154, 2020-12-3, 16.80}
        /// </example>
        /// <returns>api/teacher/addteacher</returns>
        [HttpPost]
        [Route(template:"AddTeacher")]

        public int AddTeacher([FromForm] string teacherfname, [FromForm] string teacherlname, [FromForm] string employeenumber, [FromForm] DateTime hiredate, [FromForm] float salary)
        {
            //error handling for teacher name empty
            if(string.IsNullOrEmpty(teacherfname) || string.IsNullOrEmpty(teacherlname))
            {
                return 0;
            }

            //error handling when hire date is in the future
            if (hiredate > DateTime.Now)
            {
                return 0;
            }

            //error handling for when employee # is already taken
            // is the employee number entered in the database?
            if (EmployeeNumberExists(employeenumber))
            {
                return 0;
            }


            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();

                MySqlCommand Command = Connection.CreateCommand();
                string SqlFormattedDate = hiredate.ToString("yyyy-MM-dd HH:mm:ss");

				Command.CommandText = $"INSERT INTO teachers (teacherfname, teacherlname,employeenumber, hiredate, salary ) VALUES ('{teacherfname}','{teacherlname}', '{employeenumber}', '{SqlFormattedDate}',{salary})";

				int numRowsAffected = Command.ExecuteNonQuery();
                return Convert.ToInt32(Command.LastInsertedId);

            }
        }

        //Function for error handling
        public bool EmployeeNumberExists(string employeeNumber)
        {
			using (MySqlConnection Connection = _context.AccessDatabase())
			{
				Connection.Open();

				MySqlCommand Command = Connection.CreateCommand();
                Command.CommandText = $"SELECT * FROM teachers where employeenumber = '{employeeNumber}'";

				using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    return ResultSet.HasRows;
                }

			}
		}

        /// <summary>
        /// Deletes teacher from the database. 
        /// </summary>
        /// <example>
        /// DELETE: api/TeacherPage/ListTeacher/DeleteTeacher {teacher id 20}
        /// DELETE: api/TeacherPage/ListTeacher/DeleteTeacher {teacher id 1}
        /// </example>
        /// <returns></returns>
        [HttpDelete(template:"DeleteTeacher/{teacherid}")]
        public int DeleteTeacher(int teacherid)
        {
            Teacher requestedTeacher = SingleTeacher(teacherid);

            if (requestedTeacher == null)
            {
                return 0;
            }

            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();

                MySqlCommand Command = Connection.CreateCommand();

                Command.CommandText = $"DELETE FROM teachers WHERE teacherid = {teacherid}";
                
                return Command.ExecuteNonQuery();
                
            }
        }
    }

}

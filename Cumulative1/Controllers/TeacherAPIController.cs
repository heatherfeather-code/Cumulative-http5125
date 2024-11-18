using Cumulative1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using MySql.Data.MySqlClient;


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
                        decimal Salary = Convert.ToDecimal(ResultSet["salary"]);

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
        /// GET: api/Teacher/ListTeacher/5 {Jessica Morris, 2012-06-94, T389,5}
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
                            decimal Salary = Convert.ToDecimal(ResultSet["salary"]);

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
    }

}

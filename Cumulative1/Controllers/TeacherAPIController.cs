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
        /// Returns a list of teachers from the school database
        /// </summary>
        /// <exmaple>
        /// GET api/Teacher/ ListTeacherNames [{"teacherId":1,"teacherFName": 
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
                //eatablish a query for the database
                MySqlCommand Command = Connection.CreateCommand();

                //SQL Query
                Command.CommandText = "SELECT * FROM teachers";
                //put results of query into variable:

                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
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

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <route> api/teacher/singleteacher </route>
        [HttpGet]
        [Route(template: "SingleTeacher/{teacherId}")]
        public List <Teacher> SingleTeacher(int teacherId)
        {
            //create an empty list of Teacher Names
            List<Teacher> Teachers = new List<Teacher>();
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();
                //eatablish a query for the database
                MySqlCommand Command = Connection.CreateCommand();

                //SQL Query:
                Command.CommandText = "SELECT * FROM teachers WHERE teacherid = " + teacherId;

                //put results of query into variable:

                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
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
        }
    }

}

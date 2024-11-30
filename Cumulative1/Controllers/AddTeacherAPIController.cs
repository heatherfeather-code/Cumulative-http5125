using Cumulative1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;

namespace Cumulative1.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AddTeacherAPIController : ControllerBase
	{
		private readonly SchoolDbContext _context;

		public AddTeacherAPIController(SchoolDbContext context)
		{
			_context = context;
		}
		/// <summary>
		/// This controller is designed to add a teacher to the school database
		/// </summary>
		/// <example>
		/// GET api/AddTeacherPage
		/// POST api/AddTeacherPage/teacher
		/// </example>
		/// <returns>
		/// added teacher on the database
		/// </returns>
		[HttpPost]
		[Route(template:"AddTeacher")]

		public Teacher AddTeacher()
		{
			using (MySqlConnection Connection = _context.AccessDatabase())
			{
				Connection.Open();

				MySqlCommand Command = Connection.CreateCommand();

				Command.CommandText = "INSERT INTO Teachers (teacherid,teacherfname, teacherlname, employeenumber,hiredate,salary) VALUES new.teacher";
			}
			return null;
		}
	}
}

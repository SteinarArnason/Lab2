using System;
using API.Models;
using API.Services;
using System.Collections.Generic;
using System.Web.Http;
using System.Net;
using System.Web.Http.Description;
using API.Models.Courses.Students;
using API.Services.Exceptions;

namespace Lab2.Controllers
{
	/// <summary>
	/// Controller for getting and inserting data into the database
	/// </summary>
	[RoutePrefix("api/courses")]
    public class CoursesController : ApiController
    {
		private readonly CoursesServiceProvider _service;

		/// <summary>
		/// Constructor
		/// </summary>
		public CoursesController()
		{
			_service = new CoursesServiceProvider();
		}

		/// <summary>
		/// Gets courses based on semester
		/// </summary>
		/// <param name="semester">specific semester (null is default value)</param>
		/// <returns>A list of courses</returns>
		[HttpGet]
		[Route("")]
		public List<CourseDTO> getCourses(string semester = null)
		{
			return _service.GetCoursesBySemester(semester);
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("{id}/students")]
		[ResponseType(typeof(StudentDTO))] 
		public IHttpActionResult AddStudentToCourse(int id, AddStudentViewModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var result = _service.AddStudentToCourse(id, model);
					return Content(HttpStatusCode.Created, result);
				}
				catch (AppObjectNotFoundException)
				{
					return NotFound();
				}
			}
			else
			{
				return StatusCode(HttpStatusCode.PreconditionFailed);
			}
		}

		[HttpGet]
		[Route("{id}")]
		public IHttpActionResult GetCourseByID(int id)
		{
			try
			{
				var result = _service.GetCourseByID(id);
				return Content(HttpStatusCode.OK, result);
			}
			catch (AppObjectNotFoundException)
			{
				return NotFound();
			}
		}


	}
}
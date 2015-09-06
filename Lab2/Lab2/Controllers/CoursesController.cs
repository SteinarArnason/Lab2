using System;
using API.Models;
using API.Services;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Http;
using System.Net;
using System.Web.Http.Description;
using API.Models.Courses.Students;
using API.Services.Exceptions;
using API.Models.Courses;

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
		/// Adds a student to a given course, if it fails it returns 412
		/// </summary>
		/// <param name="id">Course ID</param>
		/// <param name="model">Student we're adding to the course</param>
		/// <returns>Created(201)</returns>
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
				catch (AppPersonNotFoundException)
				{
					return StatusCode(HttpStatusCode.PreconditionFailed);
				}
			}
			else
			{
				return StatusCode(HttpStatusCode.PreconditionFailed);
			}
		}

		/// <summary>
		/// Gets a given course by its' ID returning a more detailed object
		/// about the course, if no course found returns 404
		/// </summary>
		/// <param name="id">ID of the course</param>
		/// <returns>The course we asked for</returns>
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

		/// <summary>
		/// Updates a course, if no course found returns 404
		/// </summary>
		/// <param name="id">Course ID</param>
		/// <param name="model">Updated values for the course</param>
		/// <returns>200</returns>
		[HttpPut]
		[Route("{id}")]
		[ResponseType(typeof(StudentDTO))]
		public IHttpActionResult UpdateCourseByID(int id, UpdateCourseViewModel model)
		{
			Debug.Print("hehe");
			if (ModelState.IsValid)
			{
				try
				{
					//var result = _service.UpdateCourseByID(id, model);
					//return Content(HttpStatusCode.OK, result);
					_service.UpdateCourseByID(id, model);
					return Ok();
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

		/// <summary>
		/// Deleted the given course, if no course found returns 404
		/// </summary>
		/// <param name="id">id of the course to be deleted</param>
		/// <returns>NoContent(204)</returns>
		[HttpDelete]
		[Route("{id}")]
		public IHttpActionResult DeleteCourseByID(int id)
		{
			try
			{
				_service.DeleteCourseByID(id);
				return StatusCode(HttpStatusCode.NoContent);
			}
			catch (AppObjectNotFoundException)
			{
				return NotFound();
			}
		}


	}
}
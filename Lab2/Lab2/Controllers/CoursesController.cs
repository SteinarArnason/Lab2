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

		#region Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		public CoursesController()
		{
			_service = new CoursesServiceProvider();
		}
		#endregion

		#region GET - Courses
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
		#endregion

		#region POST - Add course
		/// <summary>
		/// Add a course
		/// </summary>
		/// <param name="c">Course view model</param>
		/// <returns>201 if success and the newly created course</returns>
		[HttpPost]
		[Route("")]
		public IHttpActionResult addCourse(CourseViewModel c)
		{
			if (ModelState.IsValid)
			{
				var result = _service.AddCourse(c);
				string location = Url.Link("byID", new { id = result.ID });
				return Created(location, result);
			}
			else
			{
				return StatusCode(HttpStatusCode.PreconditionFailed);
			}
			
		}
		#endregion

		#region POST - Add student to course
		/// <summary>
		/// Adds a student to a given course
		/// If course does not exist it returns 404
		/// If person does not exist then 412
		/// </summary>
		/// <param name="id">Course ID</param>
		/// <param name="model">Student we're adding to the course</param>
		/// <returns>Created(201)</returns>
		[HttpPost]
		[Route("{id}/students")]
		public IHttpActionResult AddStudentToCourse(int id, AddStudentViewModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					_service.AddStudentToCourse(id, model);
					return StatusCode(HttpStatusCode.Created);
				}
				catch (AppObjectNotFoundException)
				{
					return NotFound();
				}
				catch (AppPersonNotFoundException)
				{
					return NotFound();
				}
				catch (AlreadyRegisteredException)
				{
					return StatusCode(HttpStatusCode.PreconditionFailed);
				}
				catch (MaxStudentsException)
				{
					return StatusCode(HttpStatusCode.PreconditionFailed);
				}
			}
			else
			{
				return StatusCode(HttpStatusCode.PreconditionFailed);
			}
		}
		#endregion

		#region GET - Get course by ID
		/// <summary>
		/// Gets a given course by its' ID returning a more detailed object
		/// about the course, if no course found returns 404
		/// </summary>
		/// <param name="id">ID of the course</param>
		/// <returns>The course we asked for</returns>
		[HttpGet]
		[Route("{id}", Name="byID")]
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
		#endregion

		#region PUT - Update course by ID
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
		#endregion

		#region DELETE - Deletes course by ID
		/// <summary>
		/// Deletes the given course, if no course found returns 404
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
		#endregion

		#region DELETE - Delete student from course by SSN
		/// <summary>
		/// Deletes a student from a course by SSN, throws appropriate exceptions that apply if an 
		/// error is encountered.
		/// </summary>
		/// <param name="id">ID of the Course</param>
		/// <param name="ssn">SSN of the Person</param>
		/// <returns>NoContent(204)</returns>
		[HttpDelete]
		[Route("{id}/students/{ssn}")]
		public IHttpActionResult DeleteStudentFromCourse(int id, string ssn)
		{
			try
			{
				_service.RemoveStudentFromCourse(id, ssn);
				return StatusCode(HttpStatusCode.NoContent);
			}
			catch (AppPersonNotFoundException)
			{
				return NotFound();
			}
			catch (AppObjectNotFoundException)
			{
				return NotFound();
			}
			catch (NotEnrolledInClassException)
			{
				return StatusCode(HttpStatusCode.PreconditionFailed);
			}
		}
		#endregion

		#region GET - Students in course
		/// <summary>
		/// Gets all the students from the course, if no course found returns 404
		/// </summary>
		/// <param name="id">id of the course we want to get students from</param>
		/// <returns>A list of students</returns>
		[HttpGet]
		[Route("{id}/students")]
		public IHttpActionResult GetStudentsInCourse(int id)
		{
			try
			{
				return Ok(_service.GetActiveStudents(id));
			}
			catch (AppObjectNotFoundException)
			{
				return NotFound();

			}
		}
		#endregion

		#region POST - Add to waiting list
		/// <summary>
		/// Adds a student to a waiting list for a specific course
		/// Handles custom exceptions in case of error
		/// </summary>
		/// <param name="id">ID of the course the waiting list is for</param>
		/// <param name="model">SSN of the person to be added to the waiting list</param>
		/// <returns>Ok(200)</returns>
		[HttpPost]
		[Route("{id}/waitinglist")]
		public IHttpActionResult AddToWaitinglist(int id, AddStudentViewModel model)
		{
			try
			{
				_service.AddStudentToWaitingList(id, model.SSN);
				return Ok();
			}
			catch (AlreadyRegisteredException)
			{
				return StatusCode(HttpStatusCode.PreconditionFailed);
			}
			catch (WaitingListException)
			{
				return StatusCode(HttpStatusCode.PreconditionFailed);
			}
			catch (AppObjectNotFoundException)
			{
				return NotFound();
			}
		}
		#endregion

		#region GET - Get waiting list
		/// <summary>
		/// Returns a list of students on the waiting list for the course
		/// </summary>
		/// <param name="id">ID of the course</param>
		/// <returns>List of students</returns>
		[HttpGet]
		[Route("{id}/waitinglist")]
		public IHttpActionResult GetWaitinglist(int id)
		{
			return Ok(_service.GetWaitinglist(id));
		}
		#endregion
	}
}
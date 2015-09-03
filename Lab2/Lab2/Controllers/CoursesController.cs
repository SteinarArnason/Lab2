using API.Models;
using API.Services;
using System.Web.Http;

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
		


	}
}
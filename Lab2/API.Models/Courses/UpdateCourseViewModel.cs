using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Courses
{

	/// <summary>
	/// Updates the start and update of the course
	/// </summary>
	public class UpdateCourseViewModel
	{
		[Required]
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
	}
}

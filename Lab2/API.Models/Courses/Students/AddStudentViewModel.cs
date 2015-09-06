using System.ComponentModel.DataAnnotations;

namespace API.Models.Courses.Students
{
	/// <summary>
	/// Adds a student to a course
	/// </summary>
	public class AddStudentViewModel
	{
		[Required]
		public string SSN { get; set; }
		public string Name { get; set; }
	}
}

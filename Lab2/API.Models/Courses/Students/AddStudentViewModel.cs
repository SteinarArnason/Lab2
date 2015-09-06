using System.ComponentModel.DataAnnotations;

namespace API.Models.Courses.Students
{
	public class AddStudentViewModel
	{
		[Required]
		public string SSN { get; set; }
		public string Name { get; set; }

	}
}

namespace API.Models.Courses.Students
{
	/// <summary>
	/// Represents a student in a list of students
	/// </summary>
	public class StudentDTO
	{
		/// <summary>
		/// Name of the student
		/// Example: "Alex Viðar Santos"
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// SSN of the student
		/// Example: 1234567890
		/// </summary>
		public string SSN { get; set; }
	}
}

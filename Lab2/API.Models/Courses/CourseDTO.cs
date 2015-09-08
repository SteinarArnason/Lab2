using System;

namespace API.Models.Courses
{
	/// <summary>
	/// Represents a course in a list of courses
	/// </summary>
	public class CourseDTO
	{
		/// <summary>
		/// Database generated unique identifier for the course
		/// Example: 12345
		/// </summary>
		public int ID { get; set; }

		/// <summary>
		/// Name of the course
		/// Example: "Vefþjónustur"
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Date when the course starts
		/// Example: "17. 08. 2015"
		/// </summary>
		public DateTime StartDate { get; set; }

		/// <summary>
		/// Number of students in the course
		/// </summary>
		public int StudentCount { get; set; }

	}
}

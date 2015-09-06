using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Models
{
	/// <summary>
	/// Represents a single course which includes more details
	/// </summary>
	public class CourseDetailsDTO
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
		/// Date when the course ends
		/// Example: "10. 11. 2015"
		/// </summary>
		public DateTime EndDate { get; set; }

		/// <summary>
		/// Specifies when the course is taught
		/// Specified by year followed by which part of the year it is
		/// Example: "20151" stands for 2015 spring
		/// Example: "20152" stands for 2015 summer
		/// Example: "20151" stands for 2015 fall
		/// </summary>
		public string Semester { get; set; }

		/// <summary>
		/// Number of students in the course
		/// </summary>
		public int StudentCount { get; set; }

	}
}

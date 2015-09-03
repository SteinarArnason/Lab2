using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab2.Models
{
	/// <summary>
	/// Represents a student enrolled in a specific course
	/// </summary>
	public class CourseRegistration
	{
		/// <summary>
		/// Unique identifier
		/// </summary>
		public int id { get; set; }

		/// <summary>
		/// Unique course identifier
		/// </summary>
		public int CourseID { get; set; }

		/// <summary>
		/// Unique student identifier
		/// </summary>
		public int StudentID { get; set; }
	}
}
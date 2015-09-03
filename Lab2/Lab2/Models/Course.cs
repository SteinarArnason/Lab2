using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab2.Models
{
	/// <summary>
	/// Represents a course in the school, 
	/// taught in a given semester
	/// </summary>
	public class Course
	{
		/// <summary>
		/// Unique identifier
		/// </summary>
		public int ID { get; set; }

		/// <summary>
		/// Identifies the template for the course
		/// Example: "T-514-VEFT"
		/// </summary>
		public string CourseID { get; set; }

		/// <summary>
		/// Date at which the course starts
		/// </summary>
		public DateTime StartDate { get; set; }

		/// <summary>
		/// Date at which the course ends
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
	}
}
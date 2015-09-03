using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab2.Models
{
	/// <summary>
	/// Represents a student in the school
	/// </summary>
	public class Student
	{
		/// <summary>
		/// Unique identifier
		/// </summary>
		public int ID { get; set; }

		/// <summary>
		/// Name of the student
		/// Example = "Jón Jónsson"
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Social Security Number
		/// Example = "1234567890"
		/// </summary>
		public string SSN { get; set; }
	}
}
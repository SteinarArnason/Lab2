using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab2.Models
{
	public class CourseTemplate
	{
		/// <summary>
		/// Identifies the template for the course
		/// Example: "T-514-VEFT"
		/// </summary>
		public string ID { get; set; }

		/// <summary>
		/// Example: "Vefþjónstur"
		/// </summary>
		public string Name { get; set; }
	}
}
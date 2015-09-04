using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.Entities
{
	/// <summary>
	/// Database class
	/// </summary>
	[Table("Courses")]
	class Course
	{
		/// <summary>
		/// Unique identifier
		/// </summary>
		public int ID { get; set; }

		/// <summary>
		/// Identifies the template for the course
		/// Example: "T-514-VEFT"
		/// </summary>
		public string TemplateID { get; set; }

		/// <summary>
		/// Date at which the course starts
		/// Example: "17. 08. 2015"
		/// </summary>
		public DateTime StartDate { get; set; }

		/// <summary>
		/// Date at which the course ends
		/// Example: "17. 08. 2015"
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

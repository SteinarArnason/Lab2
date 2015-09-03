using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Models
{
	/// <summary>
	/// Sending in a new course (create)
	/// </summary>
	public class CourseViewModel
	{
		/// <summary>
		/// ID of the course being created
		/// Example: "T-514-VEFT"
		/// </summary>
		//[Required]
		public string CourseID { get; set; }

		/// <summary>
		/// The semester the course will be taught in
		/// Example: "20153"
		/// </summary>
		public string Semester { get; set; }
	}
}

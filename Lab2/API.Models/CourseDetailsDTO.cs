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
	class CourseDetailsDTO
	{
		/// <summary>
		/// Unique identifier for this course
		/// Example: 12
		/// </summary>
		public int ID { get; set; }

		/// <summary>
		/// Name of this course
		/// Example: "Vefþjónustur"
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// A list of students registered in this course
		/// </summary>
		public List<StudentDTO> Students { get; set; } //Might want to send teachers instead, design decision

		/// <summary>
		/// A description of this course
		/// Example: "Best course you will ever have the pleasure of enlisting to"
		/// </summary>
		public string Description { get; set; }

	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.Entities
{
	/// <summary>
	/// Course templateID and Name
	/// </summary>
	class CourseTemplate
	{
		/// <summary>
		/// Unique identifier
		/// </summary>
		public int ID { get; set; }
		/// <summary>
		/// Identifies the template for the course
		/// Example: "T-514-VEFT"
		/// </summary>
		[Required]
		public string TemplateID { get; set; }

		/// <summary>
		/// Name of class
		/// Example: "Vefþjónustur"
		/// </summary>
		[Required]
		public string Name { get; set; }
	}
}

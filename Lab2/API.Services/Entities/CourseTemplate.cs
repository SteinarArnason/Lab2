using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.Entities
{
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
		public string TemplateID { get; set; }

		/// <summary>
		/// Name of class
		/// Example: "Vefþjónustur"
		/// </summary>
		public string Name { get; set; }
	}
}

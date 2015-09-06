using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.Entities
{
	/// <summary>
	/// Person that is in the school
	/// Person must exist so that we can assign it to a course
	/// </summary>
	class Person
	{
		/// <summary>
		/// Unique Identifyer
		/// </summary>
		public int ID { get; set; }
		/// <summary>
		/// Social Security Number
		/// Example: "1508932409
		/// </summary>
		public string SSN { get; set; }
		/// <summary>
		/// Name of a person
		/// Example: "Kristinn Júlíusson"
		/// </summary>
		[Required]
		public string Name { get; set; }
	}
}

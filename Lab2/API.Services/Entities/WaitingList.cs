using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.Entities
{
	class WaitingList
	{
		/// <summary>
		/// Unique identifier
		/// </summary>
		public int ID { get; set; }

		/// <summary>
		/// Id of the course that a student is on the waiting list for
		/// </summary>
		[Required]
		public int CourseID { get; set; }

		/// <summary>
		/// Id of student that is on the waiting list
		/// </summary>
		[Required]
		public int PersonID { get; set; }
	}
}

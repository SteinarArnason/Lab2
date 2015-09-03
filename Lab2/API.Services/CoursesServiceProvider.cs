using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services
{
	public class CoursesServiceProvider
	{
		/// <summary>
		/// Gets all the courses on a selected semester
		/// </summary>
		/// <param name="semester">semester specified by the user</param>
		/// <returns>A list of courses from the selected semester</returns>
		public List<CourseDTO> GetCoursesBySemester(string semester = null)
		{
			if (string.IsNullOrEmpty(semester))
			{
				semester = "20153";
			}

			//TODO finna alla áfanga sem tilheyra þessarri önn
			return null;
		}
	}
}

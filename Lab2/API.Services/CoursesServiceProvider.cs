using API.Models;
using API.Services.Repositories;
using System.Collections.Generic;
using System.Linq;
using API.Models.Courses.Students;
using System;

namespace API.Services
{
	public class CoursesServiceProvider
	{
		private readonly AppDataContext _db;

		public CoursesServiceProvider()
		{
			_db = new AppDataContext();
		}

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
			//var result = _db.Courses.Where(x => x.Semester == semester).toList(); jafngilt næstu skipun
			var result = (from c in _db.Courses
						  join ct in _db.CourseTemplates on c.TemplateID equals ct.TemplateID
						  where c.Semester == semester
						  select new CourseDTO
						  {
							  ID           = c.ID,
							  StartDate    = c.StartDate,
							  Name         = ct.Name,
							  StudentCount = 0 // TODO!!!
						 }).ToList();

			return result;
		}

		public StudentDTO AddStudentToCourse(int id, AddStudentViewModel model)
		{
			return null;
		}
	}
}

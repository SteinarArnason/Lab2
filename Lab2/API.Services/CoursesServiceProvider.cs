using API.Models;
using API.Services.Repositories;
using System.Collections.Generic;
using System.Linq;
using API.Models.Courses.Students;
using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using API.Models.Courses;
using API.Services.Entities;
using API.Services.Exceptions;

namespace API.Services
{
	public class CoursesServiceProvider
	{
		private readonly AppDataContext _db;

		public CoursesServiceProvider()
		{
			_db = new AppDataContext();
		}

		#region Get Courses by semester
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
		#endregion

		#region Add Student to course
		/// <summary>
		/// Adding Student to course
		/// </summary>
		/// <param name="id">Id of the course</param>
		/// <param name="model">SSN and Name</param>
		/// <returns></returns>
		public StudentDTO AddStudentToCourse(int id, AddStudentViewModel model)
		{

			var course = _db.Courses.SingleOrDefault(x => x.ID == id);
			if (course == null)
			{
				// if course cannot be found
				throw new AppObjectNotFoundException();
			}
			// TODO: Validate that the person exists
			var person = _db.Persons.SingleOrDefault(x => x.SSN == model.SSN);
			if (person == null)
			{
				throw new AppPersonNotFoundException();
			}
			var adding = new CourseStudent();
			adding.CourseID = course.ID;
			adding.PersonID = person.ID;
			_db.CourseStudents.Add(adding);
			_db.SaveChanges();
			return null;
		}
		#endregion

		#region Get Course by ID
		public CourseDetailsDTO GetCourseByID(int ID)
		{
			var result = _db.Courses.SingleOrDefault(x => x.ID == ID);
			if(result == null)
			{
				throw new AppObjectNotFoundException();
			}
			else
			{
				var c = new CourseDetailsDTO();
				c.ID = result.ID;
				c.Name = _db.CourseTemplates.SingleOrDefault(x => x.TemplateID == result.TemplateID).Name;
				c.StartDate = result.StartDate;
				c.EndDate = result.EndDate;
				c.StudentCount = 0;
				c.Semester = result.Semester;
				return c;
			}
		}
		#endregion

		#region Update course by id

		public void UpdateCourseByID(int id, UpdateCourseViewModel c)
		{
			Debug.Print("Inside update course by id factory");
			var results = _db.Courses.SingleOrDefault(x => x.ID == id);
			if (results == null)
			{
				throw new AppObjectNotFoundException();
			}
			results.StartDate = c.StartDate;
			results.EndDate = c.EndDate;
		
			_db.SaveChanges();
		
		} 
		#endregion

		#region Delete Course by id

		public void DeleteCourseByID(int id)
		{
			var result = _db.Courses.SingleOrDefault(x => x.ID == id);
			if (result == null)
			{
				throw new AppObjectNotFoundException();
			}

			//TODO: eyða öllum students ur course student töflunni sem að eru i þessum course
			var courseStudents = _db.CourseStudents.AsEnumerable().Where(x => x.CourseID == result.ID);

			foreach (var row in courseStudents)
			{
				_db.CourseStudents.Remove(row);
			}

			_db.Courses.Remove(result);
			_db.SaveChanges();

		}
		#endregion

		#region Get all students in course

		public List<StudentDTO> GetStudentsInCourse(int id)
		{
			var course = _db.Courses.SingleOrDefault(x => x.ID == id);
			if (course == null)
			{
				throw new AppObjectNotFoundException();
			}
			var res = (from c in _db.CourseStudents
					   join p in _db.Persons on c.PersonID equals p.ID
				       where c.CourseID == id
				       select new StudentDTO
				       {
						     Name  = p.Name,
						     SSN   = p.SSN
						}).ToList();
			return res;

		} 
		#endregion

	}
}

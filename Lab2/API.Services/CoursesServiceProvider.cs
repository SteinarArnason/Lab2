using API.Models;
using API.Services.Repositories;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using API.Models.Courses.Students;
using System.Diagnostics;
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
							  StudentCount = 0
						 }).ToList();
			foreach (var i in result)
			{
				i.StudentCount = NumberStudentsCourse(i.ID);
			}
			return result;
		}
		#endregion

		#region Add Student to course
		/// <summary>
		/// Adding Student to course
		/// Throws AppObjectNotFound if course does not exist
		/// Throws AppPersonNotFound if person does not exist
		/// </summary>
		/// <param name="id">Id of the course</param>
		/// <param name="model">SSN and Name</param>
		public StudentDTO AddStudentToCourse(int id, AddStudentViewModel model)
		{

			var course = _db.Courses.SingleOrDefault(x => x.ID == id);
			if (course == null)
			{
				// if course cannot be found
				throw new AppObjectNotFoundException();
			}
			// Checking if the person is exists
			var person = _db.Persons.SingleOrDefault(x => x.SSN == model.SSN);
			if (person == null)
			{
				throw new AppPersonNotFoundException();
			}
			var ret = new StudentDTO();
			ret.SSN = person.SSN;
			ret.Name = person.Name;

			//Checking if maximum number of student in course has been reached
			var studentsInCourse = NumberStudentsCourse(course.ID);
			if (studentsInCourse >= course.MaxStudents)
			{
				throw new MaxStudentsException();
			}
			//Removing student from waiting list
			var alreadyInCourse = _db.CourseStudents.SingleOrDefault(x => x.CourseID == course.ID && x.PersonID == person.ID);
			if (alreadyInCourse != null)
			{
				//If student is already in course but not active
				if (alreadyInCourse.Active == 0)
				{
					alreadyInCourse.Active = 1;
					_db.SaveChanges();
				}
				else
				{
					throw new AlreadyRegisteredException();
				}
				return ret;
			}
			var waiting = _db.WaitingLists.SingleOrDefault(x => x.CourseID == course.ID && x.PersonID == person.ID);
			if (waiting != null)
			{
				_db.WaitingLists.Remove(waiting);
				_db.SaveChanges();
			}
			var adding = new CourseStudent();
			adding.CourseID  = course.ID;
			adding.PersonID  = person.ID;
			adding.Active    = 1;
			_db.CourseStudents.Add(adding);
			_db.SaveChanges();
			return ret;
		}
		#endregion

		#region Get Course by ID
		/// <summary>
		/// Get course by id
		/// Throws AppObjectNotFound if course does not exist
		/// </summary>
		/// <param name="ID">id of course example 1</param>
		/// <returns>The coruse in CourseDetailsDTO</returns>
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
		/// <summary>
		/// Updates course start and end date
		/// Throws AppObjectNotFound if course does not exist
		/// </summary>
		/// <param name="id">Id of course that you want to update</param>
		/// <param name="c">New Start and end Date</param>
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
		/// <summary>
		/// Deletes course by id
		/// Throws AppObjectNotFound if course does not exist
		/// </summary>
		/// <param name="id">Id of course Example: 1</param>
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
		/// <summary>
		/// Gets all student in course
		/// Throws AppObjectNotFound if the course does not exist
		/// </summary>
		/// <param name="id">Id of the course you want to get students from</param>
		/// <returns>A list of all the students in the given course</returns>
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

		#region Number of students in course
		/// <summary>
		/// 
		/// </summary>
		/// <param name="id">Id of the course</param>
		/// <returns>Returns number of students in course</returns>
		public int NumberStudentsCourse(int id)
		{
			var course = _db.Courses.SingleOrDefault(x => x.ID == id);
			if (course == null)
			{
				throw new AppObjectNotFoundException();
			}
			return _db.CourseStudents.Count(x => x.CourseID == course.ID);
		}
		#endregion

		#region Delete student from course
		/// <summary>
		/// Remove student from course
		/// </summary>
		/// <param name="courseId">Id of the course</param>
		/// <param name="ssn">ssn of the student</param>
		public void RemoveStudentFromCourse(int courseId, string ssn)
		{
			var course = _db.Courses.SingleOrDefault(x => x.ID == courseId);
			if (course == null)
			{
				throw new AppObjectNotFoundException();
			}
			var person = _db.Persons.SingleOrDefault(x => x.SSN == ssn);
			if (person == null)
			{
				throw new AppPersonNotFoundException();
			}
			var courseStudent = _db.CourseStudents.SingleOrDefault(x => x.CourseID == course.ID && x.PersonID == person.ID);
			if (courseStudent == null)
			{
				throw new NotEnrolledInClassException();
			}
			courseStudent.Active = 0;
			//TODO:now active should be 0 but check to be sure !!!!
			//possible solution is to remove record and add it again with the correct value
			_db.SaveChanges();
		}
		#endregion

		#region Add student to waiting list
		/// <summary>
		/// Adding student to waitinglist
		/// throws AppObjectNotFound if course or person does not exist
		/// </summary>
		/// <param name="courseID">Id of the course</param>
		/// <param name="SSN">SSN of the student</param>
		public void AddStudentToWaitingList(int courseID, string SSN)
		{
			var course = _db.Courses.SingleOrDefault(x => x.ID == courseID);
			var person = _db.Persons.SingleOrDefault(x => x.SSN == SSN);
			if (course == null || person == null)
			{
				throw new AppObjectNotFoundException();
			}
			var isPersonInCourse = _db.CourseStudents.SingleOrDefault(x => x.CourseID == course.ID && x.PersonID == person.ID);
			var personInWaitingList = _db.WaitingLists.SingleOrDefault(x => x.CourseID == course.ID && x.PersonID == person.ID);
			if (isPersonInCourse.Active == 1 || personInWaitingList != null)
			{
				throw new WaitingListException();
			}

		}
		#endregion

		#region Get all active students
		/// <summary>
		/// Gets all active students in course
		/// throws AppObjectNotFound if course doesn't Exist
		/// </summary>
		/// <param name="id">Id of the course</param>
		/// <returns>Returns list of all active students in course</returns>
		public List<StudentDTO> GetActiveStudents(int id)
		{
			var course = _db.Courses.SingleOrDefault(x => x.ID == id);
			if (course == null)
			{
				throw new AppObjectNotFoundException();
			}
			var result = (from c in _db.CourseStudents
						  where c.CourseID == id && c.Active == 1
						  join ct in _db.Persons on c.PersonID equals ct.ID
						  select new StudentDTO
						  {
							  Name = ct.Name,
							  SSN = ct.SSN
						  }).ToList();
			return result;
		}
		#endregion

		#region Get students in waiting list
		/// <summary>
		/// Get all students on waitinglist
		/// </summary>
		/// <param name="id"></param>
		/// <returns>List of all students </returns>
		public List<StudentDTO> GetWaitinglist(int id)
		{
			var course = _db.Courses.SingleOrDefault(x => x.ID == id);
			if (course == null)
			{
				throw new AppObjectNotFoundException();
			}
			var result = (from c in _db.WaitingLists
						  where c.CourseID == id
						  join ct in _db.Persons on c.PersonID equals ct.ID
						  select new StudentDTO
						  {
							  Name = ct.Name,
							  SSN = ct.SSN
						  }).ToList();
			return result;
		}
		#endregion

		#region Add a course
		/// <summary>
		/// 
		/// </summary>
		/// <param name="c"></param>
		/// <returns></returns>
		public CourseDetailsDTO AddCourse(CourseViewModel c)
		{
			var addCourse = new Course
			{
				MaxStudents = c.MaxStudents,
				StartDate = c.StartDate,
				EndDate = c.EndDate,
				Semester = c.Semester,
				TemplateID = c.TemplateID,
			};
			_db.Courses.Add(addCourse);
			_db.SaveChanges();
			var getCourse = _db.Courses.SingleOrDefault(x => x.ID == addCourse.ID);
			var ret = new CourseDetailsDTO
			{
				EndDate = getCourse.EndDate,
				ID = getCourse.ID,
				Name = _db.CourseTemplates.SingleOrDefault(x => x.TemplateID == getCourse.TemplateID).Name,
				Semester = getCourse.Semester,
				StartDate = getCourse.StartDate,
				StudentCount = 0
			};
			return ret;
		}
		#endregion
	}
}

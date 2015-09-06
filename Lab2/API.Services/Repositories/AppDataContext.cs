using API.Services.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.Repositories
{
	class AppDataContext : DbContext
	{
		public DbSet<Course>         Courses         { get; set; }
		public DbSet<CourseTemplate> CourseTemplates { get; set; }
		public DbSet<Person>         Persons         { get; set; }
		public DbSet<CourseStudent>  CourseStudents  { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.Exceptions
{
	/// <summary>
	/// An instance of this class will be thrown if a student is already
	/// registered in a course/waiting list
	/// </summary>
	public class AlreadyRegisteredException : ApplicationException
	{
	}
}

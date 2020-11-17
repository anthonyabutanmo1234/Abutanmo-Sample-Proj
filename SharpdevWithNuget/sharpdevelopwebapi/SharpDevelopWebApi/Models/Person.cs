
using System;

namespace SharpDevelopWebApi.Models
{
	/// <summary>
	/// Description of Person.
	/// </summary>
	public class Person
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime? BirthDate { get; set; }
		public string Gender { get; set; }
	}
}

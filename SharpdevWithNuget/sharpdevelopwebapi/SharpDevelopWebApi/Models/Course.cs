using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace SharpDevelopWebApi.Models
{
	/// <summary>
	/// Description of Course.
	/// </summary>
	public class Course
	{   [Key]
		public string Id {get; set;}
		public string Name { get; set; }
	}
}

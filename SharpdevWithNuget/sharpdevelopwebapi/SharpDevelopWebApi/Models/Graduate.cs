using System;

using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SharpDevelopWebApi.Models
{

    public class Graduate
    {
    	public int Id { get; set; }
    	public string Firstname { get; set; }
    	public string Lastname { get; set; }
    	public string Course { get; set; }
    	public string Birthdate { get; set; }
    	public int YearGraduated { get; set; }
    	public string Status { get; set; }
    	
    }
}

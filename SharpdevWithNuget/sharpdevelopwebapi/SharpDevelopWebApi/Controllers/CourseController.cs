using System.Web;
using SharpDevelopWebApi.Helpers.JWT;
using SharpDevelopWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace SharpDevelopWebApi.Controllers
{
	/// <summary>
	/// Description of CourseController.
	/// </summary>
	public class CourseController : ApiController
	{
		SDWebApiDbContext _db = new SDWebApiDbContext();
		
		
		public IHttpActionResult Create(Course newcourse)
		{
			_db.Courses.Add(newcourse);
			_db.SaveChanges();
			return Ok(newcourse);
		}
		
		
		[HttpGet]
		public IHttpActionResult Get(string keyword)
		{
			keyword = keyword.Trim();
			var course = new List<Course>();
			
			if(!string.IsNullOrEmpty(keyword))
			{
			    
				course = _db.Courses.Where(x=> x.Name.Contains(keyword)).ToList();
			
			}
		     else
		     	course = _db.Courses.ToList();
		     return Ok(course);
		}
		
		[HttpGet]
		public IHttpActionResult Get(int Id)
		{
		  var course = 
		  	_db.Courses.Find(Id);
		  if(course !=null){
		  	return Ok(course);
		  }
		  else
		  	return BadRequest("Not Found");
		}
		
		[HttpPut]
		public IHttpActionResult Update(Course updatecourse)
		{
			var course = _db.Courses.Find(updatecourse.Id);
			if(course !=null)
			{
				course.Name=updatecourse.Name;
				
				_db.Entry(course).State = System.Data.Entity.EntityState.Modified;
				_db.SaveChanges();
				
				return Ok(course);
			
			}
			
			else
				return BadRequest("Not found...");
		}
		
		[HttpDelete]
		public IHttpActionResult Delete(int Id)
		{
			var course = _db.Courses.Find(Id);
			
			if(course !=null)
			{
				_db.Courses.Remove(course);
				_db.SaveChanges();
				
				return Ok("Course Deleted Successfully");
			}
			else 
				return BadRequest("Not Found");
		
		}
		
	}
}
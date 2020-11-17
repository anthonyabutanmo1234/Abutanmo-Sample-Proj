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
	/// Description of FacultyController.
	/// </summary>
	public class FacultyController : ApiController
	{
		
		SDWebApiDbContext _db = new SDWebApiDbContext();
		[HttpPost]
		public IHttpActionResult Create(Faculty newfaculty)
		{
			_db.Faculties.Add(newfaculty);
			_db.SaveChanges();
			return Ok(newfaculty);
		
		}
		
		[HttpGet]
		public IHttpActionResult GetAll(string keyword="")
		{
			keyword = keyword.Trim();
			
			var faculty = new List<Faculty>();
			if(!string.IsNullOrEmpty(keyword))
			{
				faculty = _db.Faculties
					.Where(x=> x.Firstname.Contains(keyword) || x.LastName.Contains(keyword))
					.ToList();
				
			}
			else
				faculty = _db.Faculties.ToList();
			
			return Ok(faculty);
		}
		
		[HttpGet]
		public IHttpActionResult Get(int Id)
		{ 
			var faculty = _db.Faculties.Find(Id);
			if(faculty != null)
			{
				return Ok(faculty);
			}
			else
				return BadRequest("Not found");
		}
		
	
		[HttpPut]
		public IHttpActionResult Update(Faculty updatefacullty)
		{
			var faculty = _db.Faculties.Find(updatefacullty);
			if( faculty != null)
			{
			
				faculty.Firstname = updatefacullty.Firstname;
				faculty.LastName = updatefacullty.LastName;
				faculty.Gender = updatefacullty.Gender;
				faculty.CivilStatus = updatefacullty.CivilStatus;
				faculty.SSSNumber = updatefacullty.SSSNumber;
				faculty.Supervisor = updatefacullty.Supervisor;
				
				_db.Entry(faculty).State = System.Data.Entity.EntityState.Modified;
				_db.SaveChanges();
				
				return Ok(faculty);
			
			}
			else
				return BadRequest("Not Found....");
		}
		
		
		[HttpDelete]
		public IHttpActionResult Delete(int Id)
		{
			var faculty = _db.Faculties.Find(Id);
			if(faculty !=null)
			{
				_db.Faculties.Remove(faculty);
				_db.SaveChanges();
				return Ok("Faculty Successfully Deleted!");
				
			
			}
			else
				return BadRequest("Faculty Not Found");
		
		}
	}
}
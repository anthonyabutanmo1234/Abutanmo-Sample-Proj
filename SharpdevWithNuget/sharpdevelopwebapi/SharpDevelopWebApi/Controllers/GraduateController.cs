using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Http;
using SharpDevelopWebApi.Models;
namespace SharpDevelopWebApi.Controllers
{
	/// <summary>
	/// Description of SongController.
	/// </summary>
	public class GraduateController :ApiController
	{
		SDWebApiDbContext _db = new SDWebApiDbContext();
		
		[HttpGet]
		 public IHttpActionResult GetAll(string keyword = "", string lastname = "",string status = "")
        {
            keyword = keyword.Trim();
            var grad = new List<Graduate>();
            
            if(!string.IsNullOrEmpty(keyword))
            {
            	grad = _db.Graduates
                    .Where(x => x.Lastname.Contains(keyword) || x.Status.Contains(keyword))
            		.ToList();
            	
                
            }
            else{
            grad = _db.Graduates.ToList();
            }
            
            if(!string.IsNullOrWhiteSpace(lastname))
            {
            	grad = grad.Where(x => x.Lastname.ToLower() == lastname.ToLower()).ToList();
            }
           
          
            
           
		 
			 return Ok(grad);
            
            
		 
        }
		
		
		public IHttpActionResult Create([FromBody]Graduate grad)
		{
			_db.Graduates.Add(grad);
			_db.SaveChanges();
			return Ok(grad.Id);
			
			
		}
		
		[HttpPut]
		public IHttpActionResult Update(Graduate updatedGrad)
		{
			var grad = _db.Graduates.Find(updatedGrad.Id);
			
			if(grad !=null)
			{
			grad.Firstname = updatedGrad.Firstname;
			grad.Lastname = updatedGrad.Lastname;
			grad.Birthdate = updatedGrad.Birthdate;
			grad.Course = updatedGrad.Course;
			grad.YearGraduated = updatedGrad.YearGraduated;
			grad.Status = updatedGrad.Status;
			
			_db.Entry(grad).State = System.Data.Entity.EntityState.Modified;
			_db.SaveChanges();
			return Ok(grad);
			}
			else
				return BadRequest("Student not Found!");
		}
		
		[Route("api/grad/{id}")]
		[HttpDelete]
		public IHttpActionResult Delete(int Id)
		{
			var grad = _db.Graduates.Find(Id);
			if(grad != null)
			{
				_db.Graduates.Remove(grad);
				_db.SaveChanges();
				return Ok("Succesfully Deleted");
			}
			else
				return BadRequest("Student not found");
		}
		[Route("api/searchstudent/{id}")]
		[HttpGet]
		public IHttpActionResult Search(int Id)
		{
			var grad = _db.Graduates.Find(Id);
			if(grad !=null)
				return Ok(grad);
				else
				return BadRequest("Not found");
		}
		
	}
}
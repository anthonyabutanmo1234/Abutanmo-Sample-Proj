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
	public class SongController :ApiController
	{
		SDWebApiDbContext _db = new SDWebApiDbContext();
		
		[HttpGet]
		 public IHttpActionResult GetAll(string keyword = "", string artist = "", int? year = null, int? peak = null)
        {
            keyword = keyword.Trim();
            var song = new List<Song>();
            
            if(!string.IsNullOrEmpty(keyword))
            {
            	song = _db.Songs
                    .Where(x => x.Title.Contains(keyword) || x.Artist.Contains(keyword))
            		.OrderBy(o => o.Title).ToList();
            	
                
            }
            else{
            song = _db.Songs.ToList();
            }
            
            if(!string.IsNullOrWhiteSpace(artist))
            {
            	song = song.Where(x => x.Artist.ToLower() == artist.ToLower()).ToList();
            }
            if(year != null)
            {
            	song = song.Where(x => x.Year == year.Value).ToList();
            }
            if(peak != null)
            {
            	song = song.Where(x => x.Peak <= peak).ToList();
            }
             int totalCount= song.Count();
		 
			 return Ok(new {totalCount, song});
            
            
		 
        }
		
		 
		 
		public IHttpActionResult Create([FromBody]Song song)
		{
			_db.Songs.Add(song);
			_db.SaveChanges();
			return Ok(song.Id);
			
			
		}
		
		
		[HttpPut]
		public IHttpActionResult Update(Song updatedSong)
		{
			var song = _db.Songs.Find(updatedSong.Id);
			
			if(song !=null)
			{
			song.Title = updatedSong.Title;
			song.Artist = updatedSong.Artist;
			song.Genre=updatedSong.Genre;
			song.Year = updatedSong.Year;
			_db.Entry(song).State = System.Data.Entity.EntityState.Modified;
			_db.SaveChanges();
			return Ok(song);
			}
			else
				return BadRequest("Song not found!");
		}
		
		[Route("api/song/{id}")]
		[HttpDelete]
		public IHttpActionResult Delete(int Id)
		{
			var song = _db.Songs.Find(Id);
			if(song != null)
			{
				_db.Songs.Remove(song);
				_db.SaveChanges();
				return Ok("Succesfully Deleted");
			}
			else
				return BadRequest("Song not found");
		}
		[Route("api/searchsong/{id}")]
		[HttpGet]
		public IHttpActionResult Search(int Id)
		{
			var song = _db.Songs.Find(Id);
			if(song !=null)
				return Ok(song);
				else
				return BadRequest("Not found");
		}
		
		
	}
}
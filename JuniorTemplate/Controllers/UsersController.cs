using JuniorTemplate.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JuniorTemplate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
{
        private SafeGuardWebContext db;

        public UsersController(SafeGuardWebContext _db)
        {
            db = _db;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public IActionResult Get()
        {
            var data = db.Users.ToList();

            if (data.Count() == 0)
                return NotFound();

            return Ok(data);
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var data = db.Users.Where(w => w.Id == id).FirstOrDefault();

            if (data == null)
                return NotFound();

            return Ok(data);
        }

        // POST api/<UsersController>
        [HttpPost]
        public IActionResult Post([FromBody] User input)
        {
            var data = db.Users.Where(w => w.Id == input.Id).FirstOrDefault();

            if (data != null)
                return Conflict();

            db.Add(input);
            db.SaveChanges();

            return Ok(input);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] User input)
        {
            var data = db.Users.Where(w => w.Id == id).FirstOrDefault();

            if (data == null)
                return NotFound();

            data.Username = input.Username;
            db.SaveChanges();

            return Ok();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var data = db.Users.Where(w => w.Id == id).FirstOrDefault();

            if (data == null)
                return NotFound();

            db.Remove(data);
            db.SaveChanges();

            return Ok();
        }
    }
}

using ProjectManagerAPI.Models;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ProjectManagerAPI.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private IProjectManagerDBEntities db = new ProjectManagerDBEntities();

        public UserController() { }

        public UserController(IProjectManagerDBEntities context)
        {
            db = context;
        }

        [Route("GetUsers")]
        public IHttpActionResult GetUsers()
        {
            if (db.Users.Count() == 0)
            {
                return NotFound();
            }

            return Ok(db.Users);
        }

        [Route("Delete")]        
        [System.Web.Http.HttpPost]
        public IHttpActionResult DeleteUser([FromBody]int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid User id");


            var user = db.Users
                .Where(s => s.UserID == id)
                .FirstOrDefault();

            db.Users.Remove(user);
            db.SaveChanges();


            return Ok();
        }

        [Route("Create")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult CreateUser(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
            return Ok();
        }

        [Route("Update")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult UpdateUser([FromBody]User user)
        {
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            try
            {
                db.SaveChanges();
                return Ok();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw;
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpGet]
        [Route("Search")]
        public IEnumerable<User> Search(string searchText)
        {
            return db.Users
                 .Where(m => searchText == null || (m.FirstName.Contains(searchText) ||
                             m.LastName.Contains(searchText) || m.EmployeeID.Contains(searchText)))
                 .OrderByDescending(m => m.UserID)
                 .ToList();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
using ProjectManagerAPI.Models;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace ProjectManagerAPI.Controllers
{
    [System.Web.Http.Cors.EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    [RoutePrefix("api/ParentTask")]
    public class ParentTaskController : ApiController
    {
        //private ProjectManagerDBEntities db = new ProjectManagerDBEntities();
        private IProjectManagerDBEntities db = new ProjectManagerDBEntities();

        public ParentTaskController() { }

        public ParentTaskController(IProjectManagerDBEntities context)
        {
            db = context;
        }

        [Route("GetParentTasks")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetParentTasks()
        {
            if (db.ParentTasks.Count() == 0)
            {
                return NotFound();
            }

            return Ok(db.ParentTasks);
        }
    }
}
using ProjectManagerAPI.Models;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace ProjectManagerAPI.Controllers
{
    [System.Web.Http.Cors.EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    [RoutePrefix("api/Task")]
    public class TaskController : ApiController
    {
        private IProjectManagerDBEntities db = new ProjectManagerDBEntities();
        //private ProjectManagerDBEntities db = new ProjectManagerDBEntities();

        public TaskController() { }
        public TaskController(IProjectManagerDBEntities context)
        {
            db = context;
        }

        [Route("GetTasks")]
        public IHttpActionResult GetTasks()
        {
            if (db.Tasks.Count() == 0)
            {
                return NotFound();
            }

            return Ok(db.Tasks);
        }

        [HttpGet]
        [Route("GetByProjectId")]
        public Task GetByProjectId(int id)
        {
            return db.Tasks
                .FirstOrDefault(m => m.ProjectID == id);
        }

        [Route("Delete")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult DeleteTask([FromBody]int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid User id");


            var task = db.Tasks
                .Where(s => s.TaskID == id)
                .FirstOrDefault();

            db.Tasks.Remove(task);
            db.SaveChanges();


            return Ok();
        }

        [Route("Create")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult CreateTask(Task task)
        {
            if (task != null)
            {
                if (task.IsParentTask)
                {
                    var parentTask = new ParentTask();
                    parentTask.ParentTaskName = task.TaskName;
                    db.ParentTasks.Add(parentTask);
                    db.SaveChanges();
                }
                else
                {

                    var taskAdded = db.Tasks.Add(task);
                    db.SaveChanges();
                    if (task.UserID.HasValue)
                    {
                        var user = db.Users.FirstOrDefault(x => x.UserID == task.UserID);
                        user.TaskID = taskAdded.TaskID;
                        user.ProjectID = taskAdded.ProjectID;
                    }
                    db.SaveChanges();
                }
            }
            return Ok();
        }

        [Route("Update")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult UpdateTask([FromBody]Task task)
        {
            db.Entry(task).State = System.Data.Entity.EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw;
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("EndTask")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult EndTask([FromBody]int id)
        {
            Task taskData = db.Tasks.Where(x => x.TaskID == id).FirstOrDefault();
            taskData.IsCompleted = true;
            db.SaveChanges();
            return Ok();
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
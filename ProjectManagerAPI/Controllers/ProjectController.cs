using ProjectManagerAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace ProjectManagerAPI.Controllers
{
    [System.Web.Http.Cors.EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    [RoutePrefix("api/Project")]
    public class ProjectController : ApiController
    {
        private IProjectManagerDBEntities db = new ProjectManagerDBEntities();
        //private ProjectManagerDBEntities db = new ProjectManagerDBEntities();

        public ProjectController() { }
        public ProjectController(IProjectManagerDBEntities context)
        {
            db = context;
        }

        [Route("Create")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult CreateProject(Project project)
        {
            Project prj = new Project();
            prj.ProjectName = project.ProjectName;
            prj.StartDate = project.StartDate;
            prj.EndDate = project.EndDate;
            prj.Priority = project.Priority;
            db.Projects.Add(prj);
            db.SaveChanges();

            if (project.managerId != null)
            {
                User UserData = db.Users.Where(x => x.UserID == project.managerId).FirstOrDefault();
                UserData.ProjectID = prj.ProjectID;
                db.SaveChanges();
            }

            return Ok();
        }

        [Route("Update")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult UpdateProject(Project project)
        {

            try
            {
                Project updateProject = new Project();
                updateProject.EndDate = project.EndDate;
                updateProject.StartDate = project.StartDate;
                updateProject.ProjectName = project.ProjectName;
                updateProject.Priority = project.Priority;
                updateProject.ProjectID = Convert.ToInt32(project.ProjectID);
                db.Entry(updateProject).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                if (project.managerId != null)
                {
                    User olduser = db.Users.Where(x => x.ProjectID == project.ProjectID).FirstOrDefault();
                    if (olduser != null)
                    {
                        olduser.ProjectID = null;
                    }
                    User UserData = db.Users.Where(x => x.UserID == project.managerId).FirstOrDefault();
                    UserData.ProjectID = updateProject.ProjectID;
                    db.SaveChanges();
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw;
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("GetProjects")]
        public IHttpActionResult GetProjects()
        {
            if (db.Projects.Count() == 0)
            {
                return NotFound();
            }

            return Ok(db.Projects);
        }

        //[Route("Delete/{id}")]
        [Route("Delete")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult DeleteProject([FromBody]int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid Project id");

            var project = db.Projects
                .Where(s => s.ProjectID == id)
                .FirstOrDefault();

            db.Projects.Remove(project);
            db.SaveChanges();
            return Ok();
        }

        [HttpGet]
        [Route("Search")]
        public IEnumerable<Project> Search(string searchText)
        {
            return db.Projects
                .Where(m => searchText == null || m.ProjectName.Contains(searchText))
                .OrderByDescending(m => m.ProjectID)
                .ToList();
        }

        [HttpGet]
        [Route("GetById")]
        public Project GetById(int id)
        {
            return db.Projects
                .FirstOrDefault(m => m.ProjectID == id);
        }
    }
}
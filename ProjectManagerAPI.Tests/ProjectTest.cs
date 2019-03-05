using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManagerAPI.Controllers;
using System.Web.Http.Results;
using System.Data.Entity;

namespace ProjectManagerAPI.Tests
{
    [TestClass]
    public class ProjectTest
    {
        //ProjectController controller = new ProjectController();

        //[TestMethod]
        //public void CreateProject()
        //{
        //    var okresult = controller.CreateProject(new Project { ProjectID = 100, ProjectName = "BPM", Priority = 1, StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(3) });
        //    Assert.IsNotNull(okresult);
        //}

        [TestMethod]
        public void GetProjects()
        {
            var context = new TestContext();
            context.Projects.Add(GetDemoProject());

            var controller = new ProjectController(context);
            var result = controller.GetProjects() as OkNegotiatedContentResult<DbSet<Project>>;
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void SaveProjects()
        {
            var context = new TestContext();
            var controller = new ProjectController(context);
            var okresult = controller.CreateProject(GetDemoProject());
            Assert.IsInstanceOfType(okresult, typeof(OkResult));
        }

        [TestMethod]
        public void UpdateProjects()
        {
            var context = new TestContext();
            var controller = new ProjectController(context);
            var okresult = controller.CreateProject(GetDemoProject());
            Assert.IsInstanceOfType(okresult, typeof(OkResult));
        }

        [TestMethod]
        public void DeleteUser()
        {
            var context = new TestContext();
            var item = GetDemoProject();
            context.Projects.Add(item);
            var controller = new ProjectController(context);
            var result = controller.DeleteProject(1) as OkResult;
            Assert.IsNotNull(result);

        }

        //ProjectDBModel GetDemoProjectModel()
        //{
        //    return new ProjectDBModel()
        //    {
        //        ProjectID = 1,
        //        EndDate = DateTime.Now,
        //        StartDate = DateTime.Now,
        //        ProjectTitle = "test",
        //        Priority = 0

        //    };
        //}

        Project GetDemoProject()
        {
            return new Project()
            {
                ProjectID = 1,
                EndDate = DateTime.Now,
                StartDate = DateTime.Now,
                ProjectName = "test",
                Priority = 0,
                managerId = null
            };
        }
    }
}

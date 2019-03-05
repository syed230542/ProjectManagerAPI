using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http.Results;
using System.Data.Entity;
using ProjectManagerAPI.Controllers;

namespace ProjectManagerAPI.Tests
{
    [TestClass]
    public class TaskTest
    {
      
        [TestMethod]
      
        public void GetTasks()
        {
            var context = new TestContext();
            context.Tasks.Add(GetDemoTask());

            var controller = new TaskController(context);
            var result = controller.GetTasks() as OkNegotiatedContentResult<DbSet<Task>>;
;
            Assert.IsNotNull(result);

       }

        [TestMethod]
        public void SaveTasks()
        {
            var context = new TestContext();
            var controller = new TaskController(context);
            var okresult = controller.CreateTask(GetDemoTask());
            Assert.IsInstanceOfType(okresult, typeof(OkResult));
        }

        [TestMethod]
        public void UpdateTasks()
        {
            var context = new TestContext();
            var controller = new TaskController(context);
            var okresult = controller.CreateTask(GetDemoTask());
            Assert.IsInstanceOfType(okresult, typeof(OkResult));
        }

        [TestMethod]
        public void DeleteTask()
        {
            var context = new TestContext();
            var item = GetDemoTask();
            context.Tasks.Add(item);
            var controller = new TaskController(context);
            var result = controller.DeleteTask(1) as OkResult;
            Assert.IsNotNull(result);

        }

        Task GetDemoTask()
        {
            return new Task() { ProjectID=1,
                EndDate=DateTime.Now,
                StartDate=DateTime.Now,
                IsParentTask=true,
                ParentID=1,
                Priority=0,
                TaskID=1,
                TaskName="",
                ParentTask=new ParentTask(),
                Project=new Project(),
                IsCompleted=false,
                User=new User(),
                UserID=1,                 
            };
        }
    }
}

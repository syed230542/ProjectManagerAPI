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

        [TestMethod]
        public void EndTask()
        {
            var context = new TestContext();
            var controller = new TaskController();
            var okresult = controller.EndTask(1005);
            Assert.IsNotNull(okresult);
        }

        [TestMethod]
        public void GetByProjectId()
        {
            var context = new TestContext();
            var controller = new TaskController();
            var okresult = controller.GetByProjectId(1002);
            Assert.IsNotNull(okresult);
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

        [TestMethod]
        public void TestUserController()
        {
            var obj = new TaskController();
            Assert.IsNotNull(obj);
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBench;
using ProjectManagerAPI.Controllers;
using ProjectManagerAPI.Tests;
using System.Web.Http.Results;
using System.Data.Entity;
using ProjectManagerAPI;

namespace ProjectManager.PerformanceTests
{
    [TestClass]
    public class PerformanceTests
    {
        private Counter _counter;
        //Test Jenkins Build

        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            _counter = context.GetCounter("TestCounter");
        }

        [Ignore]
        [TestMethod()]
        [PerfBenchmark(NumberOfIterations = 3, RunMode = RunMode.Throughput,
            RunTimeMilliseconds = 1000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestCounter", MustBe.GreaterThan, 1000)]
        [CounterTotalAssertion("TestCounter", MustBe.GreaterThan, 1000)]
        [CounterMeasurement("TestCounter")]
        public void SaveUsers()
        {
            var context = new ProjectManagerAPI.Tests.TestContext();
            var controller = new UserController(context);
            _counter.Increment();
            var okresult = controller.CreateUser(GetDemoUser());
            Assert.IsInstanceOfType(okresult, typeof(OkResult));
        }

        [Ignore]
        [TestMethod()]
        [PerfBenchmark(NumberOfIterations = 3, RunMode = RunMode.Throughput,
          RunTimeMilliseconds = 1000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestCounter", MustBe.GreaterThan, 1000)]
        [CounterTotalAssertion("TestCounter", MustBe.GreaterThan, 1000)]
        [CounterMeasurement("TestCounter")]
        public void GetUsers()
        {
            var context = new ProjectManagerAPI.Tests.TestContext();
            context.Users.Add(GetDemoUser());

            var controller = new UserController(context);
            var result = controller.GetUsers() as OkNegotiatedContentResult<DbSet<User>>;
            ;
            Assert.IsNotNull(result);

        }

        [Ignore]
        [TestMethod()]
        [PerfBenchmark(NumberOfIterations = 3, RunMode = RunMode.Throughput,
    RunTimeMilliseconds = 1000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestCounter", MustBe.GreaterThan, 1000)]
        [CounterTotalAssertion("TestCounter", MustBe.GreaterThan, 1000)]
        [CounterMeasurement("TestCounter")]
        public void SaveProjects()
        {
            var context = new ProjectManagerAPI.Tests.TestContext();
            var controller = new ProjectController(context);
            var okresult = controller.CreateProject(GetDemoProjectModel());
            Assert.IsInstanceOfType(okresult, typeof(OkResult));
        }

        [Ignore]
        [TestMethod()]
        [PerfBenchmark(NumberOfIterations = 3, RunMode = RunMode.Throughput,
 RunTimeMilliseconds = 1000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestCounter", MustBe.GreaterThan, 1000)]
        [CounterTotalAssertion("TestCounter", MustBe.GreaterThan, 1000)]
        [CounterMeasurement("TestCounter")]

        public void GetProjects()
        {
            var context = new ProjectManagerAPI.Tests.TestContext();
            context.Projects.Add(GetDemoProject());

            var controller = new ProjectController(context);
            var result = controller.GetProjects() as OkNegotiatedContentResult<DbSet<Project>>;
            ;
            Assert.IsNotNull(result);

        }


        [Ignore]
        [TestMethod()]
        [PerfBenchmark(NumberOfIterations = 3, RunMode = RunMode.Throughput,
 RunTimeMilliseconds = 1000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestCounter", MustBe.GreaterThan, 1000)]
        [CounterTotalAssertion("TestCounter", MustBe.GreaterThan, 1000)]
        [CounterMeasurement("TestCounter")]

        public void GetParents()
        {
            var context = new ProjectManagerAPI.Tests.TestContext();
            context.ParentTasks.Add(GetDemoParent());

            var controller = new ParentTaskController(context);
            var result = controller.GetParentTasks() as OkNegotiatedContentResult<DbSet<ParentTask>>;
            ;
            Assert.IsNotNull(result);

        }

        [Ignore]
        [TestMethod()]
        [PerfBenchmark(NumberOfIterations = 3, RunMode = RunMode.Throughput,
 RunTimeMilliseconds = 1000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestCounter", MustBe.GreaterThan, 1000)]
        [CounterTotalAssertion("TestCounter", MustBe.GreaterThan, 1000)]
        [CounterMeasurement("TestCounter")]

        public void GetTasks()
        {
            var context = new ProjectManagerAPI.Tests.TestContext();
            context.Tasks.Add(GetDemoTask());

            var controller = new TaskController(context);
            var result = controller.GetTasks() as OkNegotiatedContentResult<DbSet<Task>>;
            
            Assert.IsNotNull(result);

        }


        [Ignore]
        [TestMethod()]
        [PerfBenchmark(NumberOfIterations = 3, RunMode = RunMode.Throughput,
 RunTimeMilliseconds = 1000, TestMode = TestMode.Test)]
        [CounterThroughputAssertion("TestCounter", MustBe.GreaterThan, 1000)]
        [CounterTotalAssertion("TestCounter", MustBe.GreaterThan, 1000)]
        [CounterMeasurement("TestCounter")]
        public void SaveTasks()
        {
            var context = new ProjectManagerAPI.Tests.TestContext();
            var controller = new TaskController(context);
            var okresult = controller.CreateTask(GetDemoTask());
            Assert.IsInstanceOfType(okresult, typeof(OkResult));
        }

        ParentTask GetDemoParent()
        {
            return new ParentTask()
            {
                ParentID = 1,
                ParentTaskName = "test"

            };
        }

        User GetDemoUser()
        {
            return new User() { FirstName = "Syed", LastName = "Mohamed", EmployeeID = "10", ProjectID = null, TaskID = null, UserID = 1 };
        }

        Project GetDemoProjectModel()
        {
            return new Project()
            {
                ProjectID = 1,
                EndDate = DateTime.Now,
                StartDate = DateTime.Now,
                ProjectName = "test",
                Priority = 0

            };
        }

        Project GetDemoProject()
        {
            return new Project()
            {
                ProjectID = 1,
                EndDate = DateTime.Now,
                StartDate = DateTime.Now,
                ProjectName = "test",
                Priority = 0

            };
        }

        Task GetDemoTask()
        {
            return new Task()
            {
                ProjectID = 1,
                EndDate = DateTime.Now,
                StartDate = DateTime.Now,
                IsParentTask = true,
                ParentID = 1,
                Priority = 0,
                TaskID = 1,
                TaskName = "",
                ParentTask = new ParentTask(),
                Project = new Project(),
                IsCompleted = false,
                User = new User(),
                UserID = 1,
            };
        }
    }
}

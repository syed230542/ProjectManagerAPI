using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManagerAPI.Controllers;
using System.Web.Http.Results;
using System.Data.Entity;

namespace ProjectManagerAPI.Tests
{
    [TestClass]
    public class UserTest
    {
        //UserController controller = new UserController();

        //[TestMethod]
        //public void CreateUser()
        //{
        //    var okresult = controller.CreateUser(new User { UserID = 100, EmployeeID = "230542", FirstName = "Syed", LastName = "Mohamed", ProjectID =1 });
        //    Assert.IsNotNull(okresult);
        //}

        //[TestMethod]
        //public void DeleteUser()
        //{
        //    var result = controller.DeleteUser(5) as OkResult;
        //    Assert.IsNotNull(result);

        //}

        [TestMethod]

        public void GetUsers()
        {
            var context = new TestContext();
            context.Users.Add(GetDemoUser());

            var controller = new UserController(context);
            var result = controller.GetUsers() as OkNegotiatedContentResult<DbSet<User>>;
            ;
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void SaveUsers()
        {
            var context = new TestContext();
            var controller = new UserController(context);
            var okresult = controller.CreateUser(GetDemoUser());
            Assert.IsInstanceOfType(okresult, typeof(OkResult));
        }

        [TestMethod]
        public void UpdateUsers()
        {
            var context = new TestContext();
            var controller = new UserController(context);
            var okresult = controller.CreateUser(GetDemoUser());
            Assert.IsInstanceOfType(okresult, typeof(OkResult));
        }

        [TestMethod]
        public void DeleteUser()
        {
            var context = new TestContext();
            var item = GetDemoUser();
            context.Users.Add(item);
            var controller = new UserController(context);
            var result = controller.DeleteUser(1) as OkResult;
            Assert.IsNotNull(result);

        }

        User GetDemoUser()
        {
            return new User() { FirstName = "Syed", LastName = "Mohamed", EmployeeID = "10", ProjectID = null, TaskID = null, UserID = 1 };
        }
    }
}

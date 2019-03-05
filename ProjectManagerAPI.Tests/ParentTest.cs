using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http.Results;
using System.Data.Entity;
using ProjectManagerAPI.Controllers;

namespace ProjectManagerAPI.Tests
{
    [TestClass]
    public class ParentTest
    {
      
        [TestMethod]
      
        public void GetParents()
        {
            var context = new TestContext();
            context.ParentTasks.Add(GetDemoParent());

            var controller = new ParentTaskController(context);
            var result = controller.GetParentTasks() as OkNegotiatedContentResult<DbSet<ParentTask>>;
;
            Assert.IsNotNull(result);

       }

        ParentTask GetDemoParent()
        {
            return new ParentTask() { ParentID=1
                ,ParentTaskName="test"           

            };
        }
    }
}

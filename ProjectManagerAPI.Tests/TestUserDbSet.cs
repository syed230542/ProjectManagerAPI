using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagerAPI;

namespace ProjectManagerAPI.Tests
{
    class TestUserDbSet : TestDbSet<User>
    {
        public override User Find(params object[] keyValues)
        {
            return this.SingleOrDefault(User => User.UserID == (int)keyValues.Single());
        }
    }

    class TestProjectDbSet : TestDbSet<Project>
    {
        public override Project Find(params object[] keyValues)
        {
            return this.SingleOrDefault(Project => Project.ProjectID == (int)keyValues.Single());
        }
    }

    class TestTaskDbSet : TestDbSet<ProjectManagerAPI.Task>
    {
        public override ProjectManagerAPI.Task Find(params object[] keyValues)
        {
            return this.SingleOrDefault(Task => Task.TaskID == (int)keyValues.Single());
        }
    }

    class TestParentTaskDbSet : TestDbSet<ProjectManagerAPI.ParentTask>
    {
        public override ProjectManagerAPI.ParentTask Find(params object[] keyValues)
        {
            return this.SingleOrDefault(ParentTask => ParentTask.ParentID == (int)keyValues.Single());
        }
    }
}

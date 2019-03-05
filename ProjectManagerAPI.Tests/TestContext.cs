using ProjectManagerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace ProjectManagerAPI.Tests
{
    public class TestContext : IProjectManagerDBEntities
    {
        public TestContext()
        {
            this.Users = new TestUserDbSet();
            this.Projects = new TestProjectDbSet();
            this.Tasks = new TestTaskDbSet();
            this.ParentTasks = new TestParentTaskDbSet();
        }
        public DbSet<ParentTask> ParentTasks { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectManagerAPI.Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        int IProjectManagerDBEntities.SaveChanges()
        {
            return 1;
        }

        DbEntityEntry IProjectManagerDBEntities.Entry(object entity)
        {
            return null;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

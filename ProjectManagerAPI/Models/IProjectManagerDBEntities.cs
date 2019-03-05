using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


namespace ProjectManagerAPI.Models
{
    public interface IProjectManagerDBEntities : IDisposable
    {
        DbSet<ParentTask> ParentTasks { get; set; }
        DbSet<Project> Projects { get; set; }
        DbSet<Task> Tasks { get; set; }
        DbSet<User> Users { get; set; }
        int SaveChanges();
        DbEntityEntry Entry(object entity);
    }
}

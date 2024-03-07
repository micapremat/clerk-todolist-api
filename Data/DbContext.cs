using Microsoft.EntityFrameworkCore;
using Clerk_todolist_backend.Models;
using Task = Clerk_todolist_backend.Models.Task;

namespace Clerk_todolist_backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Task> Task { get; set; }
        public DbSet<Subtask> Subtask { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Subtask>()
        //        .HasOne(_ => _.Task)
        //        .WithMany(_ => _.Subtasks)
        //        .HasForeignKey(_ => _.Task.Id);
        //}
    }
}

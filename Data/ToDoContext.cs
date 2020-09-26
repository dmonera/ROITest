using Microsoft.EntityFrameworkCore;
using ROITest.Models;
using System;

namespace ROITest.Data
{
    /// <summary>
    /// DBContext and local database
    /// </summary>
    public class ToDoContext : DbContext
    {
        public ToDoContext()
        {
            this.Database.EnsureCreated();
        }

        public DbSet<ToDoModel> ToDos { get; set; }

        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=database.db");
        }
    }
}
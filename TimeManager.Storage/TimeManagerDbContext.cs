using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManager.Models;

namespace TimeManager.Storage
{
    public class TimeManagerDbContext : DbContext
    {
        public DbSet<MyTask> MyTasks { get; set; }
        public DbSet<Day> Days { get; set; }
        public TimeManagerDbContext() : base("TimeManagerDB")
        { }

    }
}

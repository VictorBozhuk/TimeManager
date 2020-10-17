using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManager.Models;

namespace TimeManager.Persistence
{
    public class TimeManagerDbContext : DbContext
    {
        public DbSet<MyTask> MyTasks { get; set; }
        public TimeManagerDbContext() : base("TimeManagerDB")
        { }

    }
}

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
        public DbSet<DailyTask> DailyTasks { get; set; }
        public DbSet<GlobalTask> GlobalTasks { get; set; }
        public DbSet<Day> Days { get; set; }
        public TimeManagerDbContext() : base("TimeManagerDB")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Day>()
                .HasMany(p => p.DailyTasks)
                .WithRequired(p => p.Day)
                .HasForeignKey(s => s.DayId)
                .WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);
        }

       
    }
}

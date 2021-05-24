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
        public DbSet<User> Users { get; set; }
        public TimeManagerDbContext() : base("TimeManagerDB")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(p => p.DailyTasks)
                .WithRequired(p => p.User)
                .HasForeignKey(s => s.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(p => p.GlobalTasks)
                .WithRequired(p => p.User)
                .HasForeignKey(s => s.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(p => p.Days)
                .WithRequired(p => p.User)
                .HasForeignKey(s => s.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Day>()
                .HasMany(p => p.DailyTasks)
                .WithRequired(p => p.Day)
                .HasForeignKey(s => s.DayId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GlobalTask>()
                .HasMany(p => p.DailyTasks)
                .WithOptional(p => p.GlobalTask)
                .HasForeignKey(s => s.GlobalTaskId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

       
    }
}

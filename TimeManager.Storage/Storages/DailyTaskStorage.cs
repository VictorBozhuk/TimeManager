using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManager.Abstracts;
using TimeManager.Models;
using TimeManager.Storage.Storages.Abstracts;

namespace TimeManager.Storage.Storages
{
    public class DailyTaskStorage : IDailyTaskStorage
    {
        private readonly TimeManagerDbContext _context;

        public DailyTaskStorage(TimeManagerDbContext context)
        {
            _context = context;
        }

        public void Create(DailyTask args)
        {
            args.Id = Guid.NewGuid();
            _context.DailyTasks.Add(args);
            _context.SaveChanges();
        }
        public DailyTask GetDailyTask(Guid id)
        {
            return GetAllDailyTasks().FirstOrDefault(x => x.Id == id);
        }
        public List<DailyTask> GetAllDailyTasks()
        {
            return _context.DailyTasks.Include("Day").AsEnumerable().OrderBy(x => x.Day.Date).ToList();
        }
        public void Edit(DailyTask args)
        {
            var myTask = GetDailyTask(args.Id);
            myTask.Title = args.Title;
            myTask.Description = args.Description;
            myTask.Status = args.Status;
            myTask.Type = args.Type;
            myTask.Start = args.Start;
            myTask.End = args.End;
            myTask.IsPlan = args.IsPlan;

            _context.SaveChanges();
        }
        public void Delete(Guid id)
        {
            var myTask = GetDailyTask(id);
            _context.DailyTasks.Remove(myTask);
            _context.SaveChanges();
        }
    }
}

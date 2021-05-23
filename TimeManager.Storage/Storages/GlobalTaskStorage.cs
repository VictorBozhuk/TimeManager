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
    public class GlobalTaskStorage : IGlobalTaskStorage
    {
        private readonly TimeManagerDbContext _context;

        public GlobalTaskStorage(TimeManagerDbContext context)
        {
            _context = context;
        }

        public void Create(GlobalTask args)
        {
            args.Id = Guid.NewGuid();

            _context.GlobalTasks.Add(args);
            _context.SaveChanges();
        }
        public GlobalTask GetGlobalTask(Guid? id)
        {
            return GetAllGlobalTasks().FirstOrDefault(x => x.Id == id);
        }
        public List<GlobalTask> GetAllGlobalTasks()
        {
            return _context.GlobalTasks.Include("DailyTasks").AsEnumerable().OrderBy(x => x.DeadLine).ToList();
        }
        public void Edit(GlobalTask args)
        {
            var myTask = GetGlobalTask(args.Id);
            myTask.Title = args.Title;
            myTask.Description = args.Description;
            myTask.Status = args.Status;
            myTask.Type = args.Type;
            myTask.IsPlan = args.IsPlan;
            myTask.DeadLine = args.DeadLine;

            _context.SaveChanges();
        }
        public void Delete(Guid id)
        {
            var myTask = GetGlobalTask(id);
            _context.GlobalTasks.Remove(myTask);
            _context.SaveChanges();
        }
    }
}

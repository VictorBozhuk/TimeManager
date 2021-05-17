using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManager.Abstracts;
using TimeManager.Models;
using TimeManager.Storage.Arguments;
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

        public void Create(GlobalTaskArgs args)
        {
            var myTask = new GlobalTask()
            {
                Id = Guid.NewGuid(),
                Title = args.Title,
                Description = args.Description,
                Status = Statuses.InProgress,
                Type = args.Type,
                IsPlan = args.IsPlan,
                DeadLine = args.DeadLine,
            };

            _context.GlobalTasks.Add(myTask);
            _context.SaveChanges();
        }
        public GlobalTask GetGlobalTask(string id)
        {
            return GetAllGlobalTasks().FirstOrDefault(x => x.Id.ToString() == id);
        }
        public List<GlobalTask> GetAllGlobalTasks()
        {
            return _context.GlobalTasks.Include("DailyTasks").AsEnumerable().OrderBy(x => x.DeadLine).ToList();
        }
        public void Edit(GlobalTaskArgs args)
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
        public void Delete(string id)
        {
            var myTask = GetGlobalTask(id);
            _context.GlobalTasks.Remove(myTask);
            _context.SaveChanges();
        }
    }
}

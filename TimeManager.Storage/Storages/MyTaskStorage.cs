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
    public class MyTaskStorage : IMyTaskStorage
    {
        private readonly TimeManagerDbContext _context;

        public MyTaskStorage(TimeManagerDbContext context)
        {
            _context = context;
        }

        public void Create(MyTaskArgs args)
        {
            var myTask = new MyTask()
            {
                Id = Guid.NewGuid(),
                Title = args.Title,
                Status = Statuses.InProgress,
                Type = args.Type,
                Start = args.Start,
                End = args.End,
                IsPlan = args.IsPlan,
                DayId = args.DayId,
            };

            _context.MyTasks.Add(myTask);
            _context.SaveChanges();
        }
        public MyTask GetMyTask(string id)
        {
            return _context.MyTasks.Include("Day").AsEnumerable().First(x => x.Id.ToString() == id);
        }
        public List<MyTask> GetAllMyTasks()
        {
            return _context.MyTasks.Include("Day").AsEnumerable().OrderBy(x => x.Day.Date).ToList();
        }
        public void Edit(MyTaskArgs args)
        {
            var myTask = GetMyTask(args.Id);
            myTask.Title = args.Title;
            myTask.Status = args.Status;
            myTask.Mark = args.Mark;
            myTask.Type = args.Type;
            myTask.Start = args.Start;
            myTask.End = args.End;

            _context.SaveChanges();
        }
        public void Delete(string id)
        {
            var myTask = GetMyTask(id);
            _context.MyTasks.Remove(myTask);
            _context.SaveChanges();
        }
    }
}

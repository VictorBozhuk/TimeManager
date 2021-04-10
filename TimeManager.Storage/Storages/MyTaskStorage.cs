using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManager.Models;
using TimeManager.Storage.Arguments;
using TimeManager.Storage.Storeges.Abstracts;

namespace TimeManager.Storage.Storeges
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
                Status = args.Status,
                Type = args.Type,
                Date = args.Date,
                Start = args.Start,
                End = args.End,
            };

            _context.MyTasks.Add(myTask);
            _context.SaveChanges();
        }
        public MyTask GetMyTask(string id)
        {
            return _context.MyTasks.AsEnumerable().First(x => x.Id.ToString() == id);
        }
        public List<MyTask> GetAllMyTasks()
        {
            return _context.MyTasks.AsEnumerable().OrderBy(x => x.Date).ToList();
        }
        public void Edit(MyTaskArgs args)
        {
            var myTask = GetMyTask(args.Id);
            myTask.Title = args.Title;
            myTask.Status = args.Status;
            myTask.Type = args.Type;
            myTask.Date = args.Date;
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

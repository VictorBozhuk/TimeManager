using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManager.Models;
using TimeManager.Storage.Storages.Abstracts;

namespace TimeManager.Storage.Storages
{
    public class DayStorage : IDayStorage
    {
        private readonly TimeManagerDbContext _context;

        public DayStorage(TimeManagerDbContext context)
        {
            _context = context;
        }

        public void Create(Day args)
        {
            args.Id = Guid.NewGuid();

            _context.Days.Add(args);
            _context.SaveChanges();
        }
        public Day GetDay(Guid id)
        {
            return _context.Days.First(x => x.Id == id);
        }
        public List<Day> GetAllDays()
        {
            return _context.Days.Include("DailyTasks").OrderByDescending(x => x.Date).ToList();
        }
        public void Edit(Day args)
        {
            var day = GetDay(args.Id);
            day.DailyTasks = args.DailyTasks;
            day.Date = args.Date;

            _context.SaveChanges();
        }
        public void Delete(Guid id)
        {
            var day = GetDay(id);
            _context.Days.Remove(day);
            _context.SaveChanges();
        }
    }
}

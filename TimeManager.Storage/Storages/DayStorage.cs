using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManager.Models;
using TimeManager.Storage.Arguments;

namespace TimeManager.Storage.Storages
{
    public class DayStorage
    {
        private readonly TimeManagerDbContext _context;

        public DayStorage(TimeManagerDbContext context)
        {
            _context = context;
        }

        public void Create(DayArgs args)
        {
            var day = new Day()
            {
                Id = Guid.NewGuid(),
                Date = args.Date,
                Tasks = args.Tasks,
            };

            _context.Days.Add(day);
            _context.SaveChanges();
        }
        public Day GetDay(string id)
        {
            return _context.Days.AsEnumerable().First(x => x.Id.ToString() == id);
        }
        public List<Day> GetAllDays()
        {
            return _context.Days.AsEnumerable().OrderBy(x => x.Date).ToList();
        }
        public void Edit(DayArgs args)
        {
            var day = GetDay(args.Id);
            day.Tasks = args.Tasks;
            day.Date = args.Date;

            _context.SaveChanges();
        }
        public void Delete(string id)
        {
            var day = GetDay(id);
            _context.Days.Remove(day);
            _context.SaveChanges();
        }
    }
}

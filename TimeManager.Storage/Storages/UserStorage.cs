using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManager.Models;
using TimeManager.Storage.Storages.Abstracts;

namespace TimeManager.Storage.Storages
{
    public class UserStorage : IUserStorage
    {
        private readonly TimeManagerDbContext _context;
        public UserStorage(TimeManagerDbContext context)
        {
            _context = context;
        }

        public void Create(User args)
        {
            args.Id = Guid.NewGuid();

            _context.Users.Add(args);
            _context.SaveChanges();
        }
        public User GetUser(Guid id)
        {
            return _context.Users.First(x => x.Id == id);
        }
        public List<User> GetAllUsers()
        {
            return _context.Users.Include("DailyTasks").Include("GlobalTasks").Include("Days").ToList();
        }
        public void Edit(User args)
        {
            var day = GetUser(args.Id);
            day.Login = args.Login;
            day.Password = args.Password;

            _context.SaveChanges();
        }
        public void Delete(Guid id)
        {
            var user = GetUser(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}

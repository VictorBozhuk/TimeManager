using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManager.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public List<DailyTask> DailyTasks { get; set; }
        public List<GlobalTask> GlobalTasks { get; set; }
        public List<Day> Days { get; set; }

        public UserModel(User user)
        {
            Id = user.Id;
            Login = user.Login;
            Password = user.Password;
            DailyTasks = user.DailyTasks;
            GlobalTasks = user.GlobalTasks;
            Days = user.Days;
        }
    }
}

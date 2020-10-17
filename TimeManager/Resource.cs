using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using TimeManager.Persistence;
using TimeManager.Persistence.Arguments;
using TimeManager.Persistence.Repository;

namespace TimeManager
{
    public class Resource
    {
        private static Resource instance;

        public Resource()
        {
            MyTaskStorage = new MyTaskStorage(new TimeManagerDbContext());

            if(!MyTaskStorage.GetAllMyTasks().Any())
            {
                MyTaskStorage.Create(new MyTaskArgs() { Name = "Name1", Type = "type1", Mark = "mark1", Date = DateTime.Now, Start = DateTime.Now, End = DateTime.Now });
                MyTaskStorage.Create(new MyTaskArgs() { Name = "Name2", Type = "type2", Mark = "mark2", Date = DateTime.Now, Start = DateTime.Now, End = DateTime.Now });
                MyTaskStorage.Create(new MyTaskArgs() { Name = "Name3", Type = "type3", Mark = "mark3", Date = DateTime.Now, Start = DateTime.Now, End = DateTime.Now });
                MyTaskStorage.Create(new MyTaskArgs() { Name = "Name4", Type = "type4", Mark = "mark4", Date = DateTime.Now, Start = DateTime.Now, End = DateTime.Now });
            }
        }

        public static Resource getInstance()
        {
            if (instance == null)
                instance = new Resource();
            return instance;
        }

        public IMyTaskStorage MyTaskStorage { get; set; }
    }
}

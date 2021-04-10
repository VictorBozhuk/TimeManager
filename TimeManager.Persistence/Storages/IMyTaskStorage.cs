using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManager.Models;
using TimeManager.Persistence.Arguments;

namespace TimeManager.Persistence.Repository
{
    public interface IMyTaskStorage
    {
        void Create(MyTaskArgs args);
        MyTask GetMyTask(string id);
        List<MyTask> GetAllMyTasks();
        void Edit(MyTaskArgs args);
        void Delete(string id);
    }
}

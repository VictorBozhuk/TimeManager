using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManager.Models;
using TimeManager.Storage.Arguments;

namespace TimeManager.Storage.Storages.Abstracts
{
    public interface IDailyTaskStorage
    {
        void Create(DailyTaskArgs args);
        DailyTask GetDailyTask(string id);
        List<DailyTask> GetAllDailyTasks();
        void Edit(DailyTaskArgs args);
        void Delete(string id);
    }
}

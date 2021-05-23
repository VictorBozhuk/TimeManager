using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManager.Models;

namespace TimeManager.Storage.Storages.Abstracts
{
    public interface IDailyTaskStorage
    {
        void Create(DailyTask args);
        DailyTask GetDailyTask(Guid id);
        List<DailyTask> GetAllDailyTasks();
        void Edit(DailyTask args);
        void Delete(Guid id);
    }
}

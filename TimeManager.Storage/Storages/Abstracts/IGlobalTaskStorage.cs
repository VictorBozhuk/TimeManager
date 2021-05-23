using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManager.Models;

namespace TimeManager.Storage.Storages.Abstracts
{
    public interface IGlobalTaskStorage
    {
        void Create(GlobalTask args);
        GlobalTask GetGlobalTask(Guid? id);
        List<GlobalTask> GetAllGlobalTasks();
        void Edit(GlobalTask args);
        void Delete(Guid id);
    }
}

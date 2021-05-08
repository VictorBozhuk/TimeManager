using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManager.Models;
using TimeManager.Storage.Arguments;

namespace TimeManager.Storage.Storages.Abstracts
{
    public interface IGlobalTaskStorage
    {
        void Create(GlobalTaskArgs args);
        GlobalTask GetGlobalTask(string id);
        List<GlobalTask> GetAllGlobalTasks();
        void Edit(GlobalTaskArgs args);
        void Delete(string id);
    }
}

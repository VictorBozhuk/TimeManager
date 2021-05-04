using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManager.Models;
using TimeManager.Storage.Arguments;

namespace TimeManager.Storage.Storages.Abstracts
{
    public interface IDayStorage
    {
        void Create(DayArgs args);
        Day GetDay(string id);
        List<Day> GetAllDays();
        void Edit(DayArgs args);
        void Delete(string id);
    }
}

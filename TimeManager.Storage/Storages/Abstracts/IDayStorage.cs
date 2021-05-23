using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManager.Models;

namespace TimeManager.Storage.Storages.Abstracts
{
    public interface IDayStorage
    {
        void Create(Day args);
        Day GetDay(Guid id);
        List<Day> GetAllDays();
        void Edit(Day args);
        void Delete(Guid id);
    }
}

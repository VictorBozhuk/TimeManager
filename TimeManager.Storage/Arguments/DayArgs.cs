using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManager.Models;

namespace TimeManager.Storage.Arguments
{
    public class DayArgs
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public List<MyTask> Tasks { get; set; }
    }
}

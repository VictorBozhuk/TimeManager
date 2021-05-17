using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManager.Storage.Arguments
{
    public class DailyTaskArgs
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public bool IsPlan { get; set; }
        public Guid DayId { get; set; }
        public Guid? GlobalTaskId { get; set; }
    }
}

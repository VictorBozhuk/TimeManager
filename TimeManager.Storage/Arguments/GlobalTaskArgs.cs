using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManager.Storage.Arguments
{
    public class GlobalTaskArgs
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public bool IsPlan { get; set; }
        public float TimeSpent { get; set; }
        public DateTime DeadLine { get; set; }
    }
}

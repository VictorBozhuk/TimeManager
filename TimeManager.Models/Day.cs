﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManager.Models
{
    public class Day
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public List<DailyTask> DailyTasks { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}

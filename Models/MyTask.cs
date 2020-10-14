﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class MyTask
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Mark { get; set; }
        public DateTime Date { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}

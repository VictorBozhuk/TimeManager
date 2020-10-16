using System;

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

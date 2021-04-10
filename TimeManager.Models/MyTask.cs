using System;
using System.Collections.Generic;

namespace TimeManager.Models
{
    public class MyTask
    {
        public Guid Id { get; set; }
        // брати шаблони, якщо ввів першу букву М, то викидає всі де є ця буква в пріорітеті ті,
        // де М перша, Молоко, що всі назви були максимально схожі
        public string Title { get; set; } 
        // Виконано, не виконано, в процесі
        public string Status { get; set; }
        // робота, дозвілля, розвиток, їжа, відпочинок
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}

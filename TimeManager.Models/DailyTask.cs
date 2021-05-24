using System;
using System.Collections.Generic;

namespace TimeManager.Models
{
    public class DailyTask
    {
        public Guid Id { get; set; }
        // брати шаблони, якщо ввів першу букву М, то викидає всі де є ця буква в пріорітеті ті,
        // де М перша, Молоко, що всі назви були максимально схожі
        public string Title { get; set; }
        public string Description { get; set; }
        // Виконано, не виконано, в процесі
        public string Status { get; set; }
        // робота, дозвілля, розвиток, їжа, відпочинок
        public string Type { get; set; }
        public bool IsPlan { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public Guid DayId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Day Day { get; set; }
        public Guid? GlobalTaskId { get; set; }
        public GlobalTask GlobalTask { get; set; }
    }
}

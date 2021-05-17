using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManager.Models
{
    public class GlobalTask
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
        public DateTime DeadLine { get; set; }
        public ICollection<DailyTask> DailyTasks { get; set; }
    }
}

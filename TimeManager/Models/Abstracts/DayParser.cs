using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManager.Models.Abstracts
{
    public static class DayParser
    {
        public static string TranslateDayOfWeek(string day)
        {
            switch (day)
            {
                case "Monday":
                    return "Понеділок";
                case "Tuesday":
                    return "Вівторок";
                case "Wednesday":
                    return "Середа";
                case "Thursday":
                    return "Четвер";
                case "Friday":
                    return "П'ятниця";
                case "Saturday":
                    return "Субота";
                case "Sunday":
                    return "Неділя";
                default:
                    return "None";
            }
        }
    }
}

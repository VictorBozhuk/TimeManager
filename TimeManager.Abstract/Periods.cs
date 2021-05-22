using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManager.Abstract
{
    public static class Periods
    {
        public const string Week = "Week";
        public const string TwoWeeks = "Two weeks";
        public const string Month = "Month";
        public const string Quarter = "Quarter";
        public const string HalfYear = "Half year";
        public const string Year = "Year";
        public const string OverYear = "Over Year";
        public const string More = "More";
        public const string Templates = "Templates";
        public const string Future = "Future";
        public const string Previous = "Previous";

        public static readonly DateTime TemplateDateTime = new DateTime(1970, 1, 1);
    }
}

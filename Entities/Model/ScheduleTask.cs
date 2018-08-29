using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Model
{
    public class ScheduleTask
    {
        public double id { get; set; }

        public string title { get; set; }

        public string start { get; set; }

        public string end { get; set; }

        public bool allDay { get; set; } = false;
    }

    public class ScheduleTaskRequestParam
    {
        public int NumberOfEmployee { get; set; }

        public DateTime[] DateRange { get; set; }

        public int Weekend { get; set; } = 1;
    }

    public enum Weekend
    {
        SaturdayAndSunday = 1,
        Sunday
    }
}

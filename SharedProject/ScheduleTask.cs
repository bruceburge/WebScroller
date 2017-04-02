using System;
using System.Collections.Generic;
using System.Text;

namespace SharedProject
{
    public class ScheduleTask
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime dueDate { get; set; }
        public string status { get; set; }
    }
}

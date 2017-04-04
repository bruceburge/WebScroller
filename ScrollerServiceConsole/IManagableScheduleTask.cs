using System;
using System.Collections.Generic;
using System.Text;


namespace ScrollerServiceConsole
{
    public interface IManagableScheduleTask
    {
        List<ScheduleTask> Tasks { get; set; }

        List<ScheduleTask> GetAllScheduleTask();
        ScheduleTask GetScheduleTaskByID(int id);
        void Load();
        void Reload();
        int RemoveScheduleTaskByID(int id);
        void Save();
    }
}
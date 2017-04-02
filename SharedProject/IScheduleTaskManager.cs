using System.Collections.Generic;

namespace SharedProject
{
    public interface IScheduleTaskManager
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
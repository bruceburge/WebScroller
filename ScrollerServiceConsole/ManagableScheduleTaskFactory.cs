using System;
using System.Collections.Generic;
using System.Text;

namespace ScrollerServiceConsole
{
    public class ManagableScheduleTaskFactory
    {
        public virtual T CreateScheduleTaskManager<T>(T scheduleTaskManager) where T : IManagableScheduleTask
        {
            //load task into list
            scheduleTaskManager.Load();
            //TODO: validation

            return scheduleTaskManager;
        }
    }
}

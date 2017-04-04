using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ScrollerServiceConsole
{
    public class ScheduleTaskManagerJsonFile : IManagableScheduleTask
    {
        private List<ScheduleTask> _tasks = new List<ScheduleTask>();
        private string taskFile = string.Empty;
        public List<ScheduleTask> Tasks
        {
            get
            {
                return _tasks;
            }

            set
            {
                _tasks = value;
            }
        }

        public ScheduleTaskManagerJsonFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Unable to validate that file exist.", filePath);
            }
            else
            {
                taskFile = filePath;
            }

        }

        public ScheduleTask GetScheduleTaskByID(int id)
        {
            return Tasks.FirstOrDefault(x => x.id == id);
        }

        /// <summary>
        /// Removes all scheduleTask with the passed ID
        /// </summary>
        /// <param name="id">Id of ScheduleTask to remove</param>
        /// <returns>Count of Elements removed.</returns>
        public int RemoveScheduleTaskByID(int id)
        {
            var results = Tasks.RemoveAll(x => x.id == id);
            if (results > 0)
            {
                Save();
            }

            return results;
        }

        public List<ScheduleTask> GetAllScheduleTask()
        {
            return Tasks;
        }

        public void Load()
        {         
            try
            {
                using (StreamReader file = File.OpenText(taskFile))
                {
                    var json = file.ReadToEnd();
                    Tasks = JsonConvert.DeserializeObject<List<ScheduleTask>>(json);
                }

            }
            catch (Exception)
            {
                throw;
            }

        }

        public void Reload()
        {

        }

        public void Save()
        {
            var JsonTasks = JsonConvert.SerializeObject(Tasks);

            Console.WriteLine(Tasks.Count());
            try
            {

                using (StreamWriter writer = new StreamWriter(taskFile, false))
                {
                    writer.WriteAsync(JsonTasks);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SharedProject
{
    public class JSONFileScheduleTaskManager : IScheduleTaskManager
    {
        private List<ScheduleTask> _tasks = new List<ScheduleTask>();

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

        public JSONFileScheduleTaskManager(string pathToJSONFile)
        {

            if(!File.Exists(pathToJSONFile))
            {
                throw new FileNotFoundException("Unable to validate that file exist.",pathToJSONFile);
            }

            try
            {
                using (StreamReader file = File.OpenText(pathToJSONFile))
                {
                    var json = System.IO.File.ReadAllText(pathToJSONFile);
                    Tasks = JsonConvert.DeserializeObject<List<ScheduleTask>>(json);                    
                }

            }
            catch (Exception)
            {
                throw;
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
            return Tasks.RemoveAll(x => x.id == id);
        }

        public List<ScheduleTask> GetAllScheduleTask()
        {
            return Tasks;
        }

        public void Load()
        {

        }

        public void Reload()
        {
            
        }

        public void Save()
        {

        }

    }
}
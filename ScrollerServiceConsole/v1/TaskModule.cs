using System;
using Nancy;
using System.Collections.Generic;
using Newtonsoft.Json;
using SharedProject;

namespace AwesomeNancySelfHost
{
    public class TaskModule : NancyModule
    {
        public TaskModule() :base("/v1")
        {

            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(location);
            var path = System.IO.Path.Combine(directory, @"resources\json\task.json");

            //this is reloaded everytime a call is made, consider static singleton, or generally better way. 
            var scheduleTaskManager = new JSONFileScheduleTaskManager(path);

            Get["/gettasks"] = parameters =>
            {
                scheduleTaskManager.Load();
                var results = scheduleTaskManager.GetAllScheduleTask();
                Console.WriteLine("# of task loaded: " + results.Count);
                return Response.AsJson(results);
            };

            //Get["/now"] = parameters =>
            //{
            //    var result = DateTime.Now.ToString();
            //    return Response.AsJson(result);
            //};

            //Get["/hello/{name}"] = parameters => {
            //   return  "Hello " + parameters.name;
            //};

            Get["/number/{id}"] = parameters => {
                string id = parameters.id;
                int numbericId;

                if (int.TryParse(id, out numbericId))
                {
                    scheduleTaskManager.RemoveScheduleTaskByID(numbericId);
                    Console.WriteLine("Id called: " + id);
                    return Response.AsJson("# " + id);
                }
                else
                {
                    return HttpStatusCode.BadRequest;
                }
                   
            };
        }
    }
}
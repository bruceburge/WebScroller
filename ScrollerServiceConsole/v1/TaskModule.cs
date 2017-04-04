using System;
using Nancy;
using System.Collections.Generic;
using Newtonsoft.Json;
using ScrollerServiceConsole;

namespace AwesomeNancySelfHost
{
    public class TaskModule : NancyModule
    {
        public TaskModule(IManagableScheduleTask manager) : base("/v1")
        {

            Get["/gettasks"] = parameters =>
            {                
                var results = manager.GetAllScheduleTask();
                Console.WriteLine("# of task loaded: " + results.Count);
                return Response.AsJson(results);
            };

            Get["/removetask/{id}"] = parameters =>
            {
                string id = parameters.id;

                if (int.TryParse(id, out int numbericId))
                {
                    manager.RemoveScheduleTaskByID(numbericId);
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
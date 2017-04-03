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
            var scheduleTaskManager = new JSONFileScheduleTaskManager(@"Z:\Users\Bruce\Documents\visual studio 2015\Projects\WebScrollerV1\SelfHostedWebAPI\resources\json\task.json");

            Get["/gettasks"] = parameters =>
            {
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
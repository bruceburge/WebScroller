using System;
using Nancy;
using System.Collections.Generic;
using Newtonsoft.Json;
using ScrollerServiceConsole;
using System.IO;
using System.Reflection;

namespace AwesomeNancySelfHost
{
    public class TaskModule : NancyModule
    {
        public TaskModule(IManagableScheduleTask manager) : base("/v1")
        {
            After.AddItemToEndOfPipeline((ctx) => ctx.Response
            .WithHeader("Access-Control-Allow-Origin", "*")
            .WithHeader("Access-Control-Allow-Methods", "POST,GET")
            .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type"));

            Get["/"] = parameters =>
            {
                string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"sharedResources\html\page.html");
                return Response.AsFile(path, "text/html");
            };

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
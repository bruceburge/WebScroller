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
                return Response.AsJson(scheduleTaskManager.GetAllScheduleTask());
            };

            Get["/now"] = parameters =>
            {
                var result = DateTime.Now.ToString();
                return Response.AsJson(result);
            };

            Get["/hello/{name}"] = parameters => {
               return  "Hello " + parameters.name;
            };

            Get["/number/{id}"] = parameters => {
                string id = parameters.id;                
                return Response.AsJson("# "+id);
            };
        }
    }
}
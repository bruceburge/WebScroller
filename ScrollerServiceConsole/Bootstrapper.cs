using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using ScrollerServiceConsole;

namespace ScrollerServiceConsole
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        // The bootstrapper enables you to reconfigure the composition of the framework,
        // by overriding the various methods and properties.
        // For more information https://github.com/NancyFx/Nancy/wiki/Bootstrapper        
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            ManagableScheduleTaskFactory scheduleTaskManagerFactory = new ManagableScheduleTaskFactory();
            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(location);
            var path = System.IO.Path.Combine(directory, @"v1\resources\json\task.json");


            //var scheduleTaskManager = scheduleTaskManagerFactory.CreateScheduleTaskManager(new ScheduleTaskManagerJsonFile(path));
            var scheduleTaskManager = scheduleTaskManagerFactory.CreateScheduleTaskManager(new ScheduleTaskManagerOutLook());

            container.Register<IManagableScheduleTask>(scheduleTaskManager);

        }
    }
}
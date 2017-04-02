using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace ScrollerServiceConsole
{
    class Program
    {
        public static void Main()
        {
            HostFactory.Run(x =>
            {
                x.Service<NancySelfHost>(s =>
                {
                    s.ConstructUsing(name => new NancySelfHost());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });

                x.RunAsLocalSystem();
                x.SetDescription("Nancy-SelfHost example");
                x.SetDisplayName("Nancy-SelfHost Service");
                x.SetServiceName("Nancy-SelfHost");
            });
        }
    }
}

﻿using Topshelf;

namespace Service.Template
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<App>(s =>
                {
                    s.ConstructUsing(name => new App());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription(AppSettings.Description);
                x.SetDisplayName(AppSettings.DisplayName);
                x.SetServiceName(AppSettings.ServiceName);
            });
        }
    }
}

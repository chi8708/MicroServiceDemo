using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ocelot;
using Ocelot.Configuration;
using Ocelot.DependencyInjection;
using Microsoft.AspNetCore;
using Ocelot.Provider.Polly;

namespace OcelotGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                  .ConfigureAppConfiguration((hostingContext, config) =>
                  {
                      config
                      .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                      .AddJsonFile("appsettings.json", true, true)
                      .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                      .AddOcelot(hostingContext.HostingEnvironment as IWebHostEnvironment)
                      .AddEnvironmentVariables();
                  })
                .ConfigureServices(s =>
                {
                    s.AddOcelot()
                     .AddPolly();//�۶�
                    
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    //add your logging
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

    }
}

using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsulDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ConsulDemo", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ConsulDemo v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            RegisterConsul();
        }

        private void RegisterConsul()
        {
            ConsulClient client = new ConsulClient(c => {
                c.Address = new Uri("http://localhost:8500/");
                c.Datacenter = "dc1";
            });

            int weight = 5;
            var id = "service1234567890";// + Guid.NewGuid();
            client.Agent.ServiceRegister(new AgentServiceRegistration()
            {
                ID =id,
                Name = "service",
                Address = "localhost",
                Port = 7001,
                Tags = new string[] { weight.ToString() },
                Check = new AgentServiceCheck()
                {
                    Interval = TimeSpan.FromSeconds(20),
                    HTTP = "http://localhost:7001/Api/Health/",
                    Timeout = TimeSpan.FromSeconds(5),
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(60)
                }
            });
      
            //×¢Ïú·þÎñ
            // client.Agent.ServiceDeregister(serviceId).Wait();
        }
    }
}

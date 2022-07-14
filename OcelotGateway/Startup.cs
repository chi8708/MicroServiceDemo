using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OcelotGateway
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OcelotGateway", Version = "v1" });
            });
           // services.AddOcelot(Configuration);
            ////������program������,ע���ļ�������programe.cs�����ú���startup ��� services.AddOcelot(Configuration)
            //services.AddOcelot(new ConfigurationBuilder()
            //.AddJsonFile("Ocelot.json")
            //.Build());


            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                builder => builder.AllowAnyOrigin().AllowAnyHeader()
                .WithMethods("GET", "POST", "HEAD", "PUT", "DELETE", "OPTIONS")
                );

            });

            //services.AddOcelot();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OcelotGateway v1"));
            }

            app.UseRouting();

            app.UseCors("AllowAll");
            app.UseAuthorization();

            //ע���м��
            var configuration = new OcelotPipelineConfiguration
            {
                PreErrorResponderMiddleware = async (ctx, next) =>
                {
                    var path= ctx.Request.Path;//����ǰ�����м��:д��־
                    await next.Invoke();
                }
            };
            app.UseOcelot(configuration).Wait();

           // app.UseOcelot().Wait();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }


    }
}

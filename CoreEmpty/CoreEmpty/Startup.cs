using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreEmpty.Models;//IStudentRepository, MockStudentRepository
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CoreEmpty
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(); //set up mvc
            //services.AddSingleton<IStudentRepository, MockStudentRepository>();// register dependency injection adding instance and removing errors
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                /*DeveloperExceptionPageOptions developerExceptionPageOptions = new DeveloperExceptionPageOptions
                {
                    SourceCodeLineCount = 2
                };*/
                app.UseDeveloperExceptionPage();
            }

            //app.UseDefaultFiles();// Call first before app.UseStaticFiles() [show default.html]
            //app.UseStaticFiles();// For the wwwroot folder
            //app.UseFileServer();// go to on default.html [It combines the functionality of UseStaticFiles() and UseDefaultFiles(). It take care of the static file as the default start page.]

            app.UseMvcWithDefaultRoute();// go to on {controller=Home}/{action=Index}/{id?}

            app.Run(async (context) =>
            {
                //throw new Exception();
                await context.Response.WriteAsync("Hello World!");
                await context.Response.WriteAsync("\nProcess Name: " + System.Diagnostics.Process.GetCurrentProcess().ProcessName);
                await context.Response.WriteAsync("\nHosting Environment: " + env.EnvironmentName);
            });
        }
    }
}

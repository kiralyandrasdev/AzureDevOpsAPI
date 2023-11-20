using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore.Hosting;
using AzureDevOps.Infrastructure;
using AzureDevOps.Application;
using AzureDevOps.API.Middlewares;
using Newtonsoft.Json;

namespace AzureDevOps.API
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
            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            services.AddSingleton<AuthorizationMiddleware>();
            services.AddSingleton<ExceptionHandlerMiddleware>();

            services.AddApplicationServices();
            services.AddInfrastructureServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseWhen(context => !context.Request.Path.StartsWithSegments("/api/connect"), appBuilder =>
            {
                appBuilder.UseMiddleware<AuthorizationMiddleware>();
            });

            app.UseMvc();
        }
    }
}

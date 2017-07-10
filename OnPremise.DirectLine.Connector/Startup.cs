using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnPremise.DirectLine.Connector
{
    public class Startup
    {
        public Task CustomOwin(IDictionary<string, object> environment)
        {
            var a = 0;
            return null;
        }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                  .SetBasePath(env.ContentRootPath)
                  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                  .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                  .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin() 
                    .AllowAnyHeader()
                    .WithMethods("GET","POST")
                    );
            });

            services.AddSession(options=>
            {
                options.CookieHttpOnly = true;
            });
            services.AddMvc();
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("CorsPolicy"));
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var console = Configuration.GetSection("Logging");            
            loggerFactory.AddConsole(console);
            loggerFactory.AddDebug();
            //app.UseOwin(pipeline =>
            //{
            //    pipeline(next => CustomOwin);
            //});

            app.UseCors("CorsPolicy");
            app.UseSession();
            app.UseMvc();
            
            

        }
    }
}

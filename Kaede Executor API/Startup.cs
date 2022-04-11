using Kaede_Executor_API.Filters;
using Kaede_Executor_API.Models.Exceptions.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Net;

namespace Kaede_Executor_API
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
            services.AddMvc(options => options.Filters.Add(new BaseExceptionFilterAttribute())).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddHttpContextAccessor();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseStatusCodePages(async context =>
            {
                var json = "";
                context.HttpContext.Response.Headers["Content-Type"] = "application/json";

                json = (HttpStatusCode)context.HttpContext.Response.StatusCode switch
                {
                    HttpStatusCode.NotFound => JsonConvert.SerializeObject(new NotFoundException()),
                    _ => json
                };

                await context.HttpContext.Response.WriteAsync(json);
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

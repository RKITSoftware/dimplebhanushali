using Microsoft.OpenApi.Models;
using Middleware_API.Middlewares;

namespace Middleware_API
{
    /// <summary>
    /// Class responsible for configuring the application's services and request pipeline.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Constructor for the Startup class.
        /// </summary>
        /// <param name="configuration">The configuration of the application.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration of the application.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures the services used by the application.
        /// </summary>
        /// <param name="services">The collection of services to configure.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Middleware", Version = "v1" });
            });
            
            var builder = WebApplication.CreateBuilder();
        }

        /// <summary>
        /// Configures the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="env">The web host environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Middleware API v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseMiddleware<AuthenticationMiddleware>();
            app.UseMiddleware<DataRetrievalMiddleware>();
            app.UseMiddleware<ConfidentialInformationMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

//Configuring Middleware Component using Use and Run Extension Method

////First Middleware Component Registered using Use Extension Method
//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("Middleware1: Incoming Request\n");
//    //Calling the Next Middleware Component
//    await next.Invoke();
//    await context.Response.WriteAsync("Middleware1: Outgoing Response\n");
//});

////Second Middleware Component Registered using Use Extension Method
//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("Middleware2: Incoming Request\n");
//    //Calling the Next Middleware Component
//    await next();
//    await context.Response.WriteAsync("Middleware2: Outgoing Response\n");
//});


////Third Middleware Component Registered using Run Extension Method
//app.Run(async (context) =>
//{
//    await context.Response.WriteAsync("Middleware3: Incoming Request handled and response generated\n");
//    //Terminal Middleware Component i.e. cannot call the Next Component
//});

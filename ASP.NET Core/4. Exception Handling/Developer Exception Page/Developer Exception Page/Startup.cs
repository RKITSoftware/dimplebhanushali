using Developer_Exception_Page.Custom_Exception_Handler;
using Microsoft.AspNetCore.Diagnostics;

namespace Developer_Exception_Page
{
    /// <summary>
    /// The startup class of the application, responsible for configuring services and the app's request pipeline.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Gets the application configuration properties.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration properties for the application.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        /// <param name="services">The service collection to which services are added.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        /// <summary>
        /// Configures the application's request pipeline.
        /// </summary>
        /// <param name="app">The application builder used to configure the request pipeline.</param>
        /// <param name="env">The web hosting environment for the application.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseDeveloperExceptionPage(new DeveloperExceptionPageOptions
            //{
            //    SourceCodeLineCount = 10
            //});

            //app.UseExceptionHandler(a => a.Run(async context =>
            //{
            //    await CustomExceptionHandler.HandleExceptionAsync(context);
            //}));

            //Using UseDeveloperExceptionPage Middleware to Show Exception Details
            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature.Error;

                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync("<html><body>\r\n");
                await context.Response.WriteAsync("Custom Error Page<br><br>\r\n");

                // Display custom error details
                await context.Response.WriteAsync($"<strong>Error:</strong> {exception.Message}<br>\r\n");
                await context.Response.WriteAsync("</body></html>\r\n");
            }));

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

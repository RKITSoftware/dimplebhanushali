using Developer_Exception_Page.Custom_Exception_Handler;
using Microsoft.AspNetCore.Diagnostics;

namespace Developer_Exception_Page
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
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage(new DeveloperExceptionPageOptions
                {
                    SourceCodeLineCount = 10
                });
            }

            //app.UseExceptionHandler(a => a.Run(async context =>
            //{
            //    await CustomExceptionHandler.HandleExceptionAsync(context);
            //}));

            //Using UseDeveloperExceptionPage Middleware to Show Exception Details
            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature.Error;

                // Custom logic for handling the exception
                // ...

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

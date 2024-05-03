namespace Routing_Web_API
{
    /// <summary>
    /// Class responsible for configuring the application's services and request pipeline.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configures the services used by the application.
        /// </summary>
        /// <param name="services">The collection of services to configure.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
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
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.UseRouting();

            // routing 

            app.UseEndpoints(endpoints =>
            {
                // All methods in controller
                endpoints.MapControllers();

                // Map the Greeting action to the /api/CLRouting endpoint with GET method
                //api/CLRouting/?name=Dimple
                endpoints.MapGet("/api/CLRouting", async context =>
                {
                    // You can handle the request logic directly here
                    string name = context.Request.Query["name"];
                    await context.Response.WriteAsync("Hello World + " + name);
                });

                // Redirect requests from api/CLRouting/hello to api/CLRouting/greetings
                endpoints.MapGet("/api/CLRouting/hello", context =>
                {
                    string name = context.Request.Query["name"];
                    context.Response.Redirect($"/api/CLRouting/greetings?name={name}");
                    return Task.CompletedTask;
                });

                // Add fallback endpoint to catch all unmatched paths
                endpoints.MapFallback(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    await context.Response.WriteAsync("Entered path does not exist !!!");
                });
            });
        }
    }
}

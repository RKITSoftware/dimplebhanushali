using Filters_Web_API.BAL;
using Filters_Web_API.Filters;
using Microsoft.OpenApi.Models;

namespace Filters_Web_API
{
    /// <summary>
    /// Configures the application's services and middleware.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class with the provided configuration.
        /// </summary>
        /// <param name="configuration">The configuration of the application.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures services for the application.
        /// </summary>
        /// <param name="services">The service collection to configure.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add controllers
            services.AddControllers();

            // Register services
            services.AddScoped<BooksService>();
            services.AddScoped<CustomActionFilter>();
            services.AddScoped<CustomAuthorizationFilter>();
            services.AddScoped<CustomExceptionFilter>();
            services.AddScoped<CustomResourceFilter>();
            services.AddScoped<CustomResultFilter>();

            // Register IMemoryCache service
            services.AddMemoryCache();

            // Add API Explorer
            services.AddEndpointsApiExplorer();

            // Configure Swagger documentation
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Filters API", Version = "v1" });

                // Add basic authentication to Swagger
                c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    In = ParameterLocation.Header,
                    Description = "Basic Authorization header using the Bearer scheme."
                });

                // Make sure Swagger UI requires a Bearer token specified
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "basic"
                            }
                        },
                        new string[] { }
                    }
                });
            });
        }

        /// <summary>
        /// Configures the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application builder to configure.</param>
        /// <param name="env">The hosting environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Enable development environment features
                app.UseDeveloperExceptionPage();

                // Enable Swagger UI
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }

            // Enable HTTPS redirection, authorization, and routing
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseRouting();

            // Map endpoints
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

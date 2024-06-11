using Microsoft.OpenApi.Models;
using MiddlewareWithFilters.BL.Interfaces;
using MiddlewareWithFilters.BL.Services;
using MiddlewareWithFilters.Data;
using MiddlewareWithFilters.Middlewares;
using MiddlewareWithFilters.Middlewares.Filters;

namespace MiddlewareWithFilters
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
            // Register your DbConnectionFactory
            services.AddSingleton<DbConnectionFactory>();

            services.AddScoped<IUSR01Service, BLUSR01Service>();

            services.AddSingleton<AuthorizationFilter>();
            services.AddSingleton<ExceptionFilter>();

            //// Add Middleware

            services.AddControllers();

            services.AddMemoryCache();

            services.AddEndpointsApiExplorer();

            // Configure Swagger documentation
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

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

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseRouting();

            app.UseExceptionMiddleware();
            app.UseAuthMiddleware();

            // Map endpoints
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

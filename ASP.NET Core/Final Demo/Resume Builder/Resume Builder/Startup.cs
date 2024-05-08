using Microsoft.OpenApi.Models;
using Resume_Builder.BL;
using Resume_Builder.BL.Interfaces;
using Resume_Builder.BL.Services;
using Resume_Builder.Data;
using Resume_Builder.DL.Implemntations;
using Resume_Builder.DL.Interfaces;
using Resume_Builder.DL.Services;
using Resume_Builder.Middlewares.Filters;
using Resume_Builder.Models.POCO;

namespace Resume_Builder
{
    public class Startup
    {
        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddHttpContextAccessor();

            services.AddSingleton<DbConnectionFactory>();

            services.AddSingleton<ICryptography,AesCrptographyService>();
            services.AddSingleton<IRedisService,RedisService>();
            services.AddSingleton<IAuthentication,AuthenticationService>();

            services.AddSingleton<IAuthService,BLAuthHandler>();

            services.AddSingleton<BLTables>();

            services.AddScoped<ICRUDService<EDU01>,CRUDImplementation<EDU01>>();
            services.AddScoped<ICRUDService<CER01>,CRUDImplementation<CER01>>();
            services.AddScoped<ICRUDService<EXP01>,CRUDImplementation<EXP01>>();
            services.AddScoped<ICRUDService<LAN01>,CRUDImplementation<LAN01>>();
            services.AddScoped<ICRUDService<PRO01>,CRUDImplementation<PRO01>>();
            services.AddScoped<ICRUDService<RES01>,CRUDImplementation<RES01>>();
            services.AddScoped<ICRUDService<SKL01>,CRUDImplementation<SKL01>>();
            services.AddScoped<ICRUDService<USR01>,CRUDImplementation<USR01>>();
            
            services.AddScoped<IEmailService,EmailService>();
            services.AddScoped<ILogging,NLogService>();

            services.AddScoped<ResumeGenerationService>();
            services.AddScoped<BulkResumeGenerationService>();
            services.AddScoped<CSVToJSON>();


            // Configures Controllers
            services.AddControllers(options =>
            {
                // Add JwtAuthenticationFilter as a global filter, excluding specific endpoint
                options.Filters.Add(typeof(JwtAuthenticationFilter));

            });

            //services.AddControllers();
            
            services.AddHttpClient("YourHttpClientName", client =>
            {
                client.Timeout = TimeSpan.FromSeconds(30); // Set a timeout of 30 seconds for both read and write operations
            });

            // Add Filters
            services.AddScoped<JwtAuthenticationFilter>();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Resume Builder API", Version = "v1" });

                // Define the BearerAuth scheme that's in use
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                // Define the requirement for BearerAuth
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }

        /// <summary>
        /// Configures the application's request pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="env">The hosting environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

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

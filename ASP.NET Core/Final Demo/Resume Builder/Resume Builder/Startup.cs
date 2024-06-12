using Microsoft.OpenApi.Models;
using Resume_Builder.BL;
using Resume_Builder.BL.Interfaces;
using Resume_Builder.BL.Services;
using Resume_Builder.Data;
using Resume_Builder.DL.Implemntations;
using Resume_Builder.DL.Interfaces;
using Resume_Builder.DL.Services;
using Resume_Builder.Helpers;
using Resume_Builder.Middlewares;
using Resume_Builder.Middlewares.Filters;
using Resume_Builder.Models.POCO;
using Resume_Builder.Services.Implemntations;
using System.Reflection;

namespace Resume_Builder
{
    /// <summary>
    /// Startup Class.
    /// </summary>
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

            // Middleware Background Service
            services.AddSingleton<RequestProcessingService>();
            services.AddHostedService<RequestProcessingService>();

            // DB COnnection Factory ORMLite
            services.AddSingleton<DbConnectionFactory>();

            // Interface And Services
            services.AddSingleton<ICryptography, AesCrptographyService>();
            services.AddSingleton<IRedisService, RedisService>();
            services.AddSingleton<IAuthentication, AuthenticationService>();
            services.AddSingleton<IAuthService, BLAuthHandler>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ILogging, NLogService>();

            services.AddSingleton<BLTables>();

            // CRUD Implementations
            services.AddScoped<ICRUDService<EDU01>, BLCRUDImplementation<EDU01>>();
            services.AddScoped<ICRUDService<CER01>, BLCRUDImplementation<CER01>>();
            services.AddScoped<ICRUDService<EXP01>, BLCRUDImplementation<EXP01>>();
            services.AddScoped<ICRUDService<LAN01>, BLCRUDImplementation<LAN01>>();
            services.AddScoped<ICRUDService<PRO01>, BLCRUDImplementation<PRO01>>();
            services.AddScoped<ICRUDService<RES01>, BLCRUDImplementation<RES01>>();
            services.AddScoped<ICRUDService<SKL01>, BLCRUDImplementation<SKL01>>();
            services.AddScoped<ICRUDService<USR01>, BLCRUDImplementation<USR01>>();

            // PDF and Image Generation Services
            services.AddScoped<BLResumeGenerationService>();
            services.AddScoped<BLBulkResumeGenerationService>();
            services.AddScoped<BLAICertificate>();

            // Add Filters
            services.AddScoped<JwtAuthenticationFilter>();

            // Configures Controllers
            services.AddControllers(options =>
            {
                // Add JwtAuthenticationFilter as a global filter, excluding specific endpoint
                options.Filters.Add(typeof(JwtAuthenticationFilter));

            })
                .AddNewtonsoftJson();

            //services.AddControllers();
            services.AddHttpClient("YourHttpClientName", client =>
            {
                client.Timeout = TimeSpan.FromSeconds(30); // Set a timeout of 30 seconds for both read and write operations
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
                    $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));

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

            services.AddSwaggerGenNewtonsoftSupport();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin();
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
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

            app.UseExceptionHandler(a => a.Run(async context =>
            {
                await CustomExceptionHandler.HandleExceptionAsync(context);
            }));

            ////Using UseDeveloperExceptionPage Middleware to Show Exception Details
            //app.UseExceptionHandler(a => a.Run(async context =>
            //{
            //    var exceptionHandlerPathFeature = context
            //                            .Features
            //                            .Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();
            //    var exception = exceptionHandlerPathFeature.Error;

            //    context.Response.ContentType = "text/html";
            //    await context.Response.WriteAsync("<html><body>\r\n");
            //    await context.Response.WriteAsync("<b>Custom Error Page</b><br><br>\r\n");

            //    // Display custom error details
            //    await context.Response.WriteAsync($"<strong>Error:</strong> {exception.Message}<br>\r\n");
            //    await context.Response.WriteAsync("</body></html>\r\n");
            //}));

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.UseRateLimitingMiddleware();

            app.UseRouting();

            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

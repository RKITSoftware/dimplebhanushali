using Certificate_Generator.BL;
using Certificate_Generator.Data;
using Certificate_Generator.Middleware;
using Microsoft.OpenApi.Models;

namespace Certificate_Generator
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

            // Register DbConnectionFactory as a singleton
            services.AddSingleton<DbConnectionFactory>();

            // Register BLUSR01Handler
            services.AddScoped<BLUSR01Handler>();
            services.AddScoped<BLCER01Handler>();
            services.AddScoped<BLCertificateGenerator>();

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Certificate Generator API", Version = "v1" });
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

            // Invoke RateLimitingMiddleware before authentication and authorization
            app.UseRateLimitingMiddleware();

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

using DILifeTime.BL.Services;
using DILifeTime.Interfaces;

namespace DILifeTime.Helpers
{
    /// <summary>
    /// Extension Class for Registration
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// extension methos which register all services in DI container 
        /// </summary>
        /// <param name="services"> collection of services </param>
        /// <returns> services </returns>
        public static IServiceCollection AddMyServices(this IServiceCollection services)
        {
            // Register services with different lifetimes
            services.AddTransient<ITransientService, OperationService>();
            services.AddScoped<IScopedService, OperationService>();
            services.AddScoped<IScopedService, ScopedService>();
            services.AddSingleton<ISingletonService, OperationService>();

            return services;
        }
    }
}

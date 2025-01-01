using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Deputies.Shared
{
    public class ResolveDependencies
    {
        private readonly IServiceProvider _serviceProvider;

        public ResolveDependencies()
        {
            // Create a fresh ServiceCollection
            var services = new ServiceCollection();

            // Build IConfiguration from appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)         // needs Microsoft.Extensions.Configuration.FileExtensions
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            // Register your services via the extension method
            services.AddDeputiesSharedServices(configuration);

            // Build the final service provider
            _serviceProvider = services.BuildServiceProvider();
        }

        // Resolve any registered service
        public T Resolve<T>() where T : notnull
            => _serviceProvider.GetRequiredService<T>();
    }
}
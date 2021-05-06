using Clean.Boundary;
using Clean.Boundary.Framework.Dependency;
using Clean.Boundary.UseCaseDependency;
using Clean.Boundary.UseCaseRequestResponse;
using Clean.UseCase;
using Dependency.Framework;
using Dependency.UseCase;
using Microsoft.Extensions.DependencyInjection;

namespace Factory
{
    /// <summary>
    /// Extension Methods for the built-in IoC container.
    /// </summary>
    public static class DependencyInjectionExtensions
    {
        /// <summary>
        /// Append the Use Cases and their dependencies to the IoC services collection.
        /// </summary>
        /// <example>
        /// public readonly IServiceProvider ServiceProvider
        ///    = new ServiceCollection()
        ///       //Add your delivery framework dependencies here
        ///       //Add the Use Cases like so...
        ///       .AddUseCases()
        ///       .BuildServiceProvider();
        /// </example>
        public static ServiceCollection AddUseCases(this ServiceCollection services)
        {
            services
                .AddTransient<IUseCaseQuery<ExampleRequest, ExampleResponse>, ExampleUseCase>()
                .AddTransient<IExampleUseCaseDependency, ExampleUseCaseDependency>()
                .AddTransient<IClock, UtcClock>();
            return services;
        }
    }
}

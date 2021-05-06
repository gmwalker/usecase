using System;
using Clean.Boundary;
using Clean.Boundary.UseCaseRequestResponse;
using Factory;
using Microsoft.Extensions.DependencyInjection;

namespace Delivery
{
    internal static class Program
    {
        private static void Main()
        {
            Console.WriteLine("Mice: Oh Deep Thought");

            var request = new ExampleRequest
            {
                Question = "What is the meaning of life, the universe, and everything?"
            };
            Console.WriteLine($"Mice: {request.Question}");

            var response = RunUseCase(request);

            Console.WriteLine($"Deep Thought: The answer is... {response.Answer}");
        }

        /// <summary>
        /// Finds Response type for Request type, and runs use case to populate the response
        /// </summary>
        private static ExampleResponse RunUseCase(ExampleRequest request)
            => RunUseCase<ExampleRequest, ExampleResponse>(_serviceProvider, request);

        /// <summary>
        /// Runs the appropriate Use Case for the Request/Response pair, and disposes the dependencies afterwards.
        /// </summary>
        /// <example>
        /// var useCaseResponse = RunUseCase&lt;ExampleUseCaseRequest, ExampleUseCaseResponse&gt;(
        ///    ServiceProvider,
        ///    new ExampleUseCaseResponse { /* Some Properties */ }
        /// );
        /// </example>
        private static TResponse RunUseCase<TRequest, TResponse>(IServiceProvider serviceProvider, TRequest request)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var useCase = scope.ServiceProvider.GetService<IUseCaseQuery<TRequest, TResponse>>();
                useCase.Request = request;
                return useCase.Execute();
            }
        }

        private static readonly IServiceProvider _serviceProvider
            = new ServiceCollection()
                .AddUseCases()
                .BuildServiceProvider();
    }
}

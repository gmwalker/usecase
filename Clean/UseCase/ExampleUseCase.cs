using System;
using Clean.Boundary.Framework;
using Clean.Boundary.Framework.Dependency;
using Clean.Boundary.UseCaseDependency;
using Clean.Boundary.UseCaseRequestResponse;
using Clean.Entity;

namespace Clean.UseCase
{
    internal class ExampleUseCase : UseCaseQuery<ExampleRequest, ExampleResponse>
    {
        protected override ExampleResponse DoWorkload()
        {
            ExampleUseCaseDependency.DoSomething();
            var deepThought = new BusinessStrategy(Request.Question);
            return new ExampleResponse
            {
                Answer = deepThought.WhatIsTheAnswer()
            };
        }

        protected override bool IsValid
            => !string.IsNullOrEmpty(Request.Question);


        #region Dependencies

        public ExampleUseCase(
            IExampleUseCaseDependency exampleUseCaseDependency,
            IClock clock = null
        )
            : base(clock)
        {
            ExampleUseCaseDependency =
                exampleUseCaseDependency
                ?? throw new ArgumentNullException(nameof(exampleUseCaseDependency));
        }

        private IExampleUseCaseDependency ExampleUseCaseDependency { get; }

        #endregion
    }
}

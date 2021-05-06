using System;
using Clean.Boundary.Framework.Dependency;

namespace Clean.Boundary.Framework
{
    internal abstract class UseCaseQuery<TRequest, TResponse>
        : IUseCaseQuery<TRequest, TResponse>
    {
        public TRequest Request { protected get; set; }

        public bool IsRequestValid
            //IDEA: Enumerate business rules
            => IsValid;

        public TResponse Execute()
        {
            //IDEA: Log
            
            //IDEA: Check Authorisation
            
            if (!IsRequestValid)
                throw new Exception("Invalid Request");
            
            return DoWorkload();
        }

        protected abstract TResponse DoWorkload();

        protected abstract bool IsValid { get; }


        #region Dependencies

        protected UseCaseQuery(IClock clock)
        {
            Clock = clock ?? throw new ArgumentNullException(nameof(clock));
        }

        protected IClock Clock;

        #endregion
    }
}
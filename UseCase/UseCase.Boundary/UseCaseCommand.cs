using System.Collections.Generic;

namespace UseCase.Boundary
{
    public abstract class UseCaseCommand : UseCaseBase, IUseCaseCommand
    {
        #region IUseCaseCommand

        public void Execute()
        {
            CheckDependencies();
            CheckAuthorisation();
            DoWorkLoad();
        }

        #endregion

        #region Protected

        protected abstract void DoWorkLoad();

        #endregion
    }

    public abstract class UseCaseCommand<TRequestModel> : UseCaseBase, IUseCaseCommand<TRequestModel>
    {
        #region IUseCaseCommand<TRequestModel>

        public void Execute()
        {
            CheckDependencies();
            CheckAuthorisation();
            CheckRequestValidation();
            DoWorkLoad();
        }

        public bool IsValidRequest
            => ValidateRequest(stopAtFirstProblem: true).Count == 0;

        public IEnumerable<ValidationProblem> ValidateRequest()
            => base.ValidateRequest();

        public TRequestModel Request { protected get; set; }

        #endregion

        #region Protected

        protected abstract void DoWorkLoad();

        #endregion
    }

}
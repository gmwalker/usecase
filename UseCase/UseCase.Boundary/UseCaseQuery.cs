using System.Collections.Generic;

namespace UseCase.Boundary
{
    public abstract class UseCaseQuery<TResponseModel> : UseCaseBase, IUseCaseQuery<TResponseModel>
    {
        #region IUseCaseQuery<TResponseModel>

        public TResponseModel Execute()
        {
            CheckDependencies();
            CheckAuthorisation();
            return DoWorkLoad();
        }

        #endregion

        #region Protected

        protected abstract TResponseModel DoWorkLoad();

        #endregion
    }


    public abstract class UseCaseQuery<TRequestModel, TResponseModel> : UseCaseBase, IUseCaseQuery<TRequestModel, TResponseModel>
    {
        #region IUseCaseQuery<TRequestModel, TResponseModel>

        public TResponseModel Execute()
        {
            CheckDependencies();
            CheckAuthorisation();
            CheckRequestValidation();
            return DoWorkLoad();
        }

        public bool IsValidRequest => ValidateRequest(stopAtFirstProblem: true).Count == 0;

        public IEnumerable<ValidationProblem> ValidateRequest()
            => base.ValidateRequest();

        public TRequestModel Request { protected get; set; }

        #endregion

        #region Protected

        protected abstract TResponseModel DoWorkLoad();

        #endregion
    }
}
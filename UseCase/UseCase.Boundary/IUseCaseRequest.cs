using System.Collections.Generic;

namespace UseCase.Boundary
{
    public interface IUseCaseRequest<in TRequestModel>
    {
        TRequestModel Request { set; }

        bool IsValidRequest { get; }

        IEnumerable<ValidationProblem> ValidateRequest();
    }
}
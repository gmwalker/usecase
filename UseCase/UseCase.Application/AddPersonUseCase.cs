using System;
using UseCase.Application.RequestResponse;
using UseCase.Boundary;

namespace UseCase.Application
{
    public class AddPersonUseCase : UseCaseCommand<AddPersonRequest>
    {
        protected override void DoWorkLoad()
        {
            throw new System.NotImplementedException();
        }

        protected override ValidationRules ValidationRules
            => new ValidationRules
            {
                { () => Request.DateOfBirth <= DateTime.Today, "Born in the future?" },
            };
    }
}

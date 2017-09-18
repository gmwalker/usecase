using System;
using UseCase.Boundary;
using UseCase.PhoneBook.RequestResponse;

namespace UseCase.PhoneBook
{
    public class AddPersonUseCase : UseCaseCommand<AddPersonRequest>
    {
        protected override void DoWorkLoad()
        {
            /*
             * TODO: Add Person
             */
            throw new NotImplementedException();
        }

        protected override ValidationRules ValidationRules
            => new ValidationRules
            {
                { () => Request.DateOfBirth > DateTime.Today, "Born in the future?" },
            };
    }
}

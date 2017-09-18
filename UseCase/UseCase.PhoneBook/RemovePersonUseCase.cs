using System;
using UseCase.Boundary;

namespace UseCase.PhoneBook
{
    public class RemovePersonUseCase : UseCaseCommand
    {
        protected override void DoWorkLoad()
        {
            /*
             * Storage - Cascade Delete
             * Storage - Add to register of deleted persons
             * Notify Admin
             * Notify Coordinators - list of changed rosters
             * Notify Person - optional (default: no)
             */
            throw new NotImplementedException();
        }
    }
}
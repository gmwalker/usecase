using System;
using UseCase.Boundary;

namespace UseCase.PhoneBook
{
    public class MakePersonActiveUseCase : UseCaseCommand
    {
        protected override void DoWorkLoad()
        {
            /*
             * Storage - Set Person's status to Active
             * Notify Admin
             * Notify Coordinators - list of changed rosters
             * Notify Person - optional (default: no)
             */
            throw new NotImplementedException();
        }
    }
}
using System;
using UseCase.Boundary;

namespace UseCase.PhoneBook
{
    public class MakePersonInactiveUseCase : UseCaseCommand
    {
        protected override void DoWorkLoad()
        {
            /*
             * Storage - Delete roster(s) for person
             * Storage - Delete person from team(s)?
             * Storage - Set Person's status to Inactive
             * Notify Admin
             * Notify Coordinators - list of changed rosters
             * Notify Person - optional (default: no)
             */
            throw new NotImplementedException();
        }
    }
}
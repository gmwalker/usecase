using System;
using UseCase.Boundary;
using UseCase.PhoneBook.RequestResponse;

namespace UseCase.PhoneBook
{
    public class SearchForPersonUseCase : UseCaseQuery<SearchForPersonRequest, SearchForPersonResponse>
    {
        protected override SearchForPersonResponse DoWorkLoad()
        {
            /*
             * Storage - Search
             */
            throw new NotImplementedException();
        }
    }
}
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UseCase.PhoneBook;
using UseCase.PhoneBook.RequestResponse;
using UseCase.Boundary;

namespace UseCase.Test.PhonebookTests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void AddPersonValidationTest()
        {
            var addPersonUseCase = UseCaseFactory.Make(NormalAddPersonRequest);
            Assert.IsTrue(addPersonUseCase.IsValidRequest);
        }

        [TestMethod]
        public void AddPerson_BornInTheFuture()
        {
            var bogusAddPersonRequestWithDobInTheFuture = NormalAddPersonRequest;
            bogusAddPersonRequestWithDobInTheFuture.DateOfBirth = new DateTime(2018, 5, 8);

            var addPersonUseCase = UseCaseFactory.Make(bogusAddPersonRequestWithDobInTheFuture);
            Assert.IsFalse(addPersonUseCase.IsValidRequest);
            foreach (var validationProblem in addPersonUseCase.ValidateRequest())
                Console.WriteLine(validationProblem.Message);
        }

        #region Private

        private static AddPersonRequest NormalAddPersonRequest
            => new AddPersonRequest
            {
                FirstName = "Bad",
                Surname = "Bart",
                DateOfBirth = new DateTime(2007, 5, 8),
                Gender = Gender.Male,
                IsPublicallyAvailable = true,
                Username = "bbart",
                Password = "The Simpsons"
            };

        #endregion
    }

    internal static class UseCaseFactory
    {
        internal static IUseCaseCommand<AddPersonRequest> Make(AddPersonRequest request)
            => new AddPersonUseCase { Request = request };
    }
}

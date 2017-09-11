using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UseCase.Application;
using UseCase.Application.RequestResponse;
using UseCase.Boundary;

namespace UseCase.Test.Application
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void AddPersonValidationTest()
        {
            var useCase =
                (IUseCaseCommand<AddPersonRequest>)
                new AddPersonUseCase
                {
                    Request = new AddPersonRequest
                    {
                        FirstName = "Bad",
                        Surname = "Bart",
                        DateOfBirth = new DateTime(2007, 5, 8),
                        Gender = Gender.Male,
                        IsPublicallyAvailable = true,
                        Username = "bbart",
                        Password = "The Simpsons"
                    }
                };

            Assert.IsTrue(useCase.IsValidRequest);

        }

        [TestMethod]
        public void AddPerson_BornInTheFuture()
        {
            var useCase =
                (IUseCaseCommand<AddPersonRequest>)
                new AddPersonUseCase
                {
                    Request = new AddPersonRequest
                    {
                        FirstName = "Bad",
                        Surname = "Bart",
                        DateOfBirth = new DateTime(2018, 5, 8),
                        Gender = Gender.Male,
                        IsPublicallyAvailable = true,
                        Username = "bbart",
                        Password = "The Simpsons"
                    }
                };

            Assert.IsFalse(useCase.IsValidRequest);
            
            var problems = useCase.ValidateRequest();

            foreach (var problem in problems)
            {
                Console.WriteLine(problem);
            }
        }
    }
}

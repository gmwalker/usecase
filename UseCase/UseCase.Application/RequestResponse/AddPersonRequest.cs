using System;

namespace UseCase.Application.RequestResponse
{
    public class AddPersonRequest
    {
        public string FirstName { get; set; }

        public string MiddleNames { get; set; }

        public string Surname { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public DateTime DateOfBirth { get; set; }

        public  Gender Gender{ get; set; }

        public bool IsPublicallyAvailable { get; set; }

    }
}
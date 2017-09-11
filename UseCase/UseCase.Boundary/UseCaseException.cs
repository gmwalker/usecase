using System;

namespace UseCase.Boundary
{
    /// <summary>
    /// This class exists for the purposes of exception filtering in the delivery mechanisms; 
    /// and therefore as a base-class for all use-case exceptions.
    /// </summary>
    public class UseCaseException : Exception
    {
        public UseCaseException() {}
        public UseCaseException(string message, bool isUserFacing) : base(message) { IsUserFacing = isUserFacing; }
        public UseCaseException(string message, Exception innerException, bool isUserFacing) : base(message, innerException) { IsUserFacing = isUserFacing; }
        public bool IsUserFacing { get; protected set; }
    }

    public class UseCaseValidationException : UseCaseException
    {
        public UseCaseValidationException(string message, bool isUserFacing) : base(message, isUserFacing) { }
        public UseCaseValidationException(string message, Exception innerException, bool isUserFacing) : base(message, innerException, isUserFacing ) { }
    }

    public class UseCaseDependencyException : UseCaseException
    {
        public UseCaseDependencyException(string message, bool isUserFacing) : base(message, isUserFacing) { }
        public UseCaseDependencyException(string message, Exception innerException, bool isUserFacing) : base(message, innerException, isUserFacing) { }
    }
}

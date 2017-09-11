using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UseCase.Boundary.Dependency;
using Entity.Authorisation;

namespace UseCase.Boundary
{
    public abstract class UseCaseBase : IUseCase
    {
        #region IUseCase

        public virtual bool IsAuthorised
            =>
                Permissions.Count == 0
                ||
                Permissions.All(permission => AccessControlList.CanUserDo(new AclUser { Id = AuthorisationContext.UserAccountId }, permission));

        #endregion


        #region Properties

        /// <summary>
        /// Obtain the user context from the delivery mechanism
        /// </summary>
        public AuthorisationContext AuthorisationContext { protected get; set; }

        #endregion


        #region Protected Virtual (Used and extended/overwritten by child classes)

        /// <summary>
        /// Override when the UseCase has permissions
        /// </summary>
        protected virtual HashSet<AclPermission> Permissions
            => new HashSet<AclPermission>();

        /// <summary>
        /// Overide to check dependencies are available.  Don't forget to call base.CheckDependencies()
        /// </summary>
        protected virtual void CheckDependencies()
        {
            if (LazyLoggingProvider == null) throw new UseCaseException("Lazy Logging Provider dependency missing", isUserFacing: false);
            if (LazyAccessControlList == null) throw new UseCaseException("Lazy Access Control List dependency missing", isUserFacing: false);
        }

        /// <summary>
        /// Override if the validation rules are too complex to express in the ValidationRules property.
        /// </summary>
        protected virtual List<ValidationProblem> ValidateRequest(bool stopAtFirstProblem = false)
        {
            var problems = new List<ValidationProblem>();

            foreach (var rule in ValidationRules)
            {
                // ReSharper disable once InvertIf - easier to read this way
                if (rule.Fails())
                {
                    problems.Add(rule.Problem);
                    if (stopAtFirstProblem)
                        return problems;
                }
            }

            return problems;
        }

        /// <summary>
        /// Override to supply a set of boolean validation rules for the Request.
        /// Override the ValidateRequest() method if the validation rules are too complex.
        /// </summary>
        protected virtual ValidationRules ValidationRules { get; } = new ValidationRules();

        #endregion


        #region Protected (Used by child classes)

        /// <summary>
        /// Check if the user is authorised to run this UseCase, and throw if they are unauthorised.
        /// </summary>
        protected void CheckAuthorisation()
        {
            if (!IsAuthorised) throw new UseCaseException("Unauthorised", isUserFacing: true);
        }

        /// <summary>
        /// Check if the request is valid, and throw if invalid
        /// </summary>
        protected void CheckRequestValidation()
        {
            var problems = ValidateRequest(stopAtFirstProblem: true);
            if (problems.Count > 0) throw new UseCaseValidationException($"Invalid Request: {problems[0].Message}", problems[0].IsUserFacing);
        }

        protected IAccessControlList AccessControlList
            => LazyAccessControlList.Value;

        protected ILoggingProvider LoggingProvider
            => LazyLoggingProvider.Value;

        #endregion


        #region Dependencies

        public Lazy<IAccessControlList> LazyAccessControlList { private get; set; }

        public Lazy<ILoggingProvider> LazyLoggingProvider { private get; set; }

        #endregion
    }


    #region Authorisation Classes

    public class AuthorisationContext
    {
        public int UserAccountId { get; set; }

        public int TenantId { get; set; }
    }

    #endregion


    #region Validation Classes

    /// <summary>
    /// Basic collection of ValidationRule's with support for object initialiser syntax.
    /// </summary>
    public class ValidationRules : IEnumerable<ValidationRule>
    {
        public void Add(Func<bool> fails, string message, bool isUserFacing = true)
            => _validationRules.Add(new ValidationRule { Fails = fails, Problem = new ValidationProblem { Message = message, IsUserFacing = isUserFacing } });

        public void Add(ValidationRule validationRule)
            => _validationRules.Add(validationRule);

        public IEnumerator<ValidationRule> GetEnumerator()
            => _validationRules.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => _validationRules.GetEnumerator();

        private readonly List<ValidationRule> _validationRules = new List<ValidationRule>();
    }

    public struct ValidationRule
    {
        public Func<bool> Fails;
        public ValidationProblem Problem;
    }

    public struct ValidationProblem
    {
        public string Message;
        public bool IsUserFacing;
    }

    #endregion
}
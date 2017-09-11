using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Entity.Authorisation
{
    /// <summary>
    /// The Access-Control-List (ACL) for a given set of Policies.
    /// An ACL is a list of everything permitted for every user - User, Securable, Operation
    /// </summary>
    public class AccessControlList : IAccessControlList
    {
        #region Implementation of IAccessControlList

        public bool CanUserDo(AclUser user, AclPermission permission)
            => ApplyPolicies(p => p.IsAllowed(user, permission), p => p.IsDenied(user, permission));

        public HashSet<AclPermission> WhatCanUserDo(AclUser user)
            => ApplyPolicies(p => p.PermissionsAllowed(user), p => p.PermissionsDenied(user));

        public HashSet<AclOperation> WhatCanUserDoTo(AclUser user, AclSecurable obj)
            => ApplyPolicies(p => p.OperationsAllowed(user, obj), p => p.OperationsDenied(user, obj));

        public HashSet<AclUser> WhichUsersCanDo(AclPermission permission)
            => ApplyPolicies(p => p.UserAccountsAllowed(permission), p => p.UserAccountsDenied(permission));

        public HashSet<AclSecurable> ToWhatCanUserDo(AclUser user, AclOperation operation)
            => ApplyPolicies(policy => policy.SecurablesAllowed(user, operation), policy => policy.SecurablesDenied(user, operation));

        #endregion

        #region Private

        private HashSet<T> ApplyPolicies<T>(Func<Policy, HashSet<T>> allowed, Func<Policy, HashSet<T>> denied)
        {
            var result = new HashSet<T>();
            //Add the results allowed by the policy
            foreach (var policy in Policies) result.UnionWith(allowed(policy));
            //If no results are allowed by the policy, there's no reason to check the "denies"
            if (result.Count == 0) return result;
            //Subtract the results denied by the policy
            foreach (var policy in Policies) result.ExceptWith(denied(policy));
            return result;
        }

        [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalse")]
        [SuppressMessage("ReSharper", "RedundantBoolCompare")]
        private bool ApplyPolicies(Func<Policy, bool> allowed, Func<Policy, bool> denied)
        {
            var isAllowed = false;
            //If any policy "allows" then it is allowed - break the loop early
            foreach (var policy in Policies) if ((isAllowed = isAllowed || allowed(policy)) == true) break;
            //If no policy "allows" there's no reason to check the "denies"
            if (isAllowed == false) return isAllowed;
            //If any policy "denies" then it is denied - break the loop early
            foreach (var policy in Policies) if ((isAllowed = isAllowed && denied(policy) == false) == false) break;
            return isAllowed;
        }

        #endregion

        #region Setters

        public List<Policy> Policies { private get; set; }

        #endregion

    }


    /// <summary>
    /// The base class for all Policies.
    /// Policies are combined to produce the Access Control List.
    /// By default all the methods in the base class do nothing; they allow nothing and they deny nothing.
    /// Policies which derive from this base class will implement relevent members by combining business rules with underlying data.
    /// </summary>
    public abstract class Policy
    {
        #region Allowed

        public virtual bool IsAllowed(AclUser userAccount, AclPermission permission)
            => false;

        public virtual HashSet<AclOperation> OperationsAllowed(AclUser userAccount, AclSecurable securable)
            => new HashSet<AclOperation>();

        public virtual HashSet<AclPermission> PermissionsAllowed(AclUser userAccount)
            => new HashSet<AclPermission>();

        public virtual HashSet<AclUser> UserAccountsAllowed(AclPermission permission)
            => new HashSet<AclUser>();

        public virtual HashSet<AclSecurable> SecurablesAllowed(AclUser user, AclOperation operation)
            => new HashSet<AclSecurable>();

        #endregion

        #region Denied

        public virtual bool IsDenied(AclUser userAccount, AclPermission permission)
            => false;

        public virtual HashSet<AclOperation> OperationsDenied(AclUser userAccount, AclSecurable securable)
            => new HashSet<AclOperation>();

        public virtual HashSet<AclPermission> PermissionsDenied(AclUser userAccount)
            => new HashSet<AclPermission>();

        public virtual HashSet<AclUser> UserAccountsDenied(AclPermission permission)
            => new HashSet<AclUser>();

        public virtual HashSet<AclSecurable> SecurablesDenied(AclUser user, AclOperation operation)
            => new HashSet<AclSecurable>();

        #endregion
    }
}
using System.Collections.Generic;
using Entity.Authorisation;

namespace UseCase.Boundary.Dependency.Stub
{
    public class AlwaysAllowedAccessControlList : IAccessControlList
    {
        public bool CanUserDo(AclUser user, AclPermission permission)
            => true;

        public HashSet<AclPermission> WhatCanUserDo(AclUser user)
            => new HashSet<AclPermission>();

        public HashSet<AclOperation> WhatCanUserDoTo(AclUser user, AclSecurable securable)
            => new HashSet<AclOperation>();

        public HashSet<AclUser> WhichUsersCanDo(AclPermission permission)
            => new HashSet<AclUser>();

        public HashSet<AclSecurable> ToWhatCanUserDo(AclUser user, AclOperation operation)
            => new HashSet<AclSecurable>();
    }
}

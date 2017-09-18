using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace - This is where I want it
namespace Entity.Authorisation
{
    public interface IAccessControlList
    {
        bool CanUserDo(AclUser user, AclPermission permission);
        HashSet<AclPermission> WhatCanUserDo(AclUser user);
        HashSet<AclOperation> WhatCanUserDoTo(AclUser user, AclSecurable obj);
        HashSet<AclUser> WhichUsersCanDo(AclPermission permission);
        HashSet<AclSecurable> ToWhatCanUserDo(AclUser user, AclOperation operation);
    }

    #region Types

    public class AclUser : IEquatable<AclUser>
    {
        #region Equality

        public bool Equals(AclUser other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        //[ExcludeFromCodeCoverage]
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AclUser)obj);
        }

        //[ExcludeFromCodeCoverage]
        public override int GetHashCode() => Id;

        //[ExcludeFromCodeCoverage]
        public static bool operator ==(AclUser left, AclUser right) => Equals(left, right);

        //[ExcludeFromCodeCoverage]
        public static bool operator !=(AclUser left, AclUser right) => !Equals(left, right);

        #endregion

        #region ToString

        public new string ToString()
            => $"{Description} (UserAccount {Id})";

        #endregion

        public int Id { get; set; }

        public string Description { get; set; }
    }

    public class AclPermission : IEquatable<AclPermission>
    {
        #region Equality

        public bool Equals(AclPermission other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Securable.Equals(other.Securable) && Operation.Equals(other.Operation);
        }

        //[ExcludeFromCodeCoverage]
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AclPermission)obj);
        }

        //[ExcludeFromCodeCoverage]
        public override int GetHashCode()
        {
            unchecked { return (Securable.GetHashCode() * 397) ^ Operation.GetHashCode(); }
        }

        //[ExcludeFromCodeCoverage]
        public static bool operator ==(AclPermission left, AclPermission right) => Equals(left, right);

        //[ExcludeFromCodeCoverage]
        public static bool operator !=(AclPermission left, AclPermission right) => !Equals(left, right);

        #endregion

        #region ToString

        public override string ToString()
            => $"{Operation.Description} {Securable}";

        #endregion

        public AclSecurable Securable { get; set; }

        public AclOperation Operation { get; set; }
    }

    //[Serializable]
    public class AclOperation : IEquatable<AclOperation>
    {
        #region Equality

        public bool Equals(AclOperation other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        //[ExcludeFromCodeCoverage]
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AclOperation)obj);
        }

        //[ExcludeFromCodeCoverage]
        public override int GetHashCode()
        {
            return Id;
        }

        //[ExcludeFromCodeCoverage]
        public static bool operator ==(AclOperation left, AclOperation right)
        {
            return Equals(left, right);
        }

        //[ExcludeFromCodeCoverage]
        public static bool operator !=(AclOperation left, AclOperation right)
        {
            return !Equals(left, right);
        }

        #endregion

        #region ToString

        public override string ToString()
            => $"{Description} - {SecurableType} {Id}";

        #endregion

        public int Id { get; set; }

        public SecurableType SecurableType { get; set; }

        public string Description { get; set; }
    }

    public class AclSecurable : IEquatable<AclSecurable>
    {
        #region Equality

        public bool Equals(AclSecurable other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Type == other.Type && Id == other.Id;
        }

        //[ExcludeFromCodeCoverage]
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AclSecurable)obj);
        }

        //[ExcludeFromCodeCoverage]
        public override int GetHashCode()
        {
            unchecked
            {
                return ((int)Type * 397) ^ Id;
            }
        }

        //[ExcludeFromCodeCoverage]
        public static bool operator ==(AclSecurable left, AclSecurable right) => Equals(left, right);

        //[ExcludeFromCodeCoverage]
        public static bool operator !=(AclSecurable left, AclSecurable right) => !Equals(left, right);

        #endregion

        #region ToString

        public override string ToString()
            => $"\"{Description}\" ({Type} {Id})";

        #endregion

        public SecurableType Type { get; set; }

        public int Id
        {
            get
            {
                if (_objectId == null) throw new AuthorisationException("Has not been initialised");
                return _objectId.Value;
            }
            set
            {
                if (_objectId != null) throw new AuthorisationException("Already initialised");
                _objectId = value;
            }
        }

        public string Description { get; set; }

        #region Private

        private int? _objectId;

        #endregion
    }

    #endregion


    #region Enums

    //Add types for each kind of securable you have
    public enum SecurableType
    {
        NotSet,
    }

    #endregion


    #region Exceptions

    public class AuthorisationException : Exception
    {
        //[ExcludeFromCodeCoverage]
        public AuthorisationException() { }

        //[ExcludeFromCodeCoverage]
        public AuthorisationException(string message) : base(message) { }

        //[ExcludeFromCodeCoverage]
        public AuthorisationException(string message, Exception innerException) : base(message, innerException) { }
    }

    #endregion
}
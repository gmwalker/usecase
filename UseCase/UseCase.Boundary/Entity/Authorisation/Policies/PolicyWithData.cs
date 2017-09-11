namespace Entity.Authorisation.Policies
{
    public abstract class PolicyWithAuthorisationData : Policy
    {
        public AuthorisationData AuthorisationData { protected get; set; }
    }

    //TODO: Add data required by your authorisation policies to determine who can do what
    public class AuthorisationData
    {
    }
}

using Entity.Authorisation.Policies;

namespace Entity.Authorisation.Dependency
{
    public interface IAuthorisationStorage
    {
        AuthorisationData Read();
    }
}

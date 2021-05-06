namespace Clean.Boundary
{
    public interface IUseCaseQuery<in TRequest, out TResponse>
    {
        TRequest Request { set; }

        bool IsRequestValid { get; }

        TResponse Execute();
    }
}
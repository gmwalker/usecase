namespace UseCase.Boundary
{
    public interface IUseCaseCommand<in TRequestModel> : IUseCaseCommand, IUseCaseRequest<TRequestModel>
    {
    }

    public interface IUseCaseCommand : IUseCase
    {
        void Execute();
    }
}
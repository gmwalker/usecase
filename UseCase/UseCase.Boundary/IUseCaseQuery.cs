namespace UseCase.Boundary
{
    public interface IUseCaseQuery<in TRequestModel, out TResponseModel> : IUseCaseQuery<TResponseModel>, IUseCaseRequest<TRequestModel>
    {
    }

    public interface IUseCaseQuery<out TResponseModel> : IUseCase
    {
        TResponseModel Execute();
    }
}

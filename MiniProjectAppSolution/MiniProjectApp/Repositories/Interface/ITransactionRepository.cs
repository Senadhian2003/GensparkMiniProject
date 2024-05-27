namespace MiniProjectApp.Repositories.Interface
{
    public interface ITransactionRepository
    {
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();

    }
}

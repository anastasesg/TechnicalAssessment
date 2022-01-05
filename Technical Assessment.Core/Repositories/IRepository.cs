namespace Technical_Assessment.Core.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> QueryAsync(string sql, object? param = null, CancellationToken cancellationToken = default);
        
        Task<T> QueryFirstOrDefaultAsync(string sql, object? param = null, CancellationToken cancellationToken = default);

        Task<T> QuerySingleAsync(string sql, object? param = null, CancellationToken cancellationToken = default);

        Task<int> ExecuteAsync(string sql, object? param = null, CancellationToken cancellationToken = default);

        void Dispose();
    }
}

using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using Technical_Assessment.Core.Repositories;
using Technical_Assessment.Infrastructure.Data;

namespace Technical_Assessment.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IDbConnection connection;
        private readonly IApplicationDbContext context;

        public Repository(IConfiguration configuration, IApplicationDbContext context)
        {
            this.context = context;
            connection = new SqlConnection(configuration.GetConnectionString("SMSDb"));
        }

        public void Dispose() => connection.Dispose();

        public async Task<int> ExecuteAsync(string sql, object? param = null, CancellationToken cancellationToken = default)
        {
            return await context.Connection.ExecuteAsync(sql, param);
        }

        public async Task<IReadOnlyList<T>> QueryAsync(string sql, object? param = null, CancellationToken cancellationToken = default)
        {
            return (await connection.QueryAsync<T>(sql, param)).AsList();
        }

        public async Task<T> QueryFirstOrDefaultAsync(string sql, object? param = null, CancellationToken cancellationToken = default)
        {
            return await connection.QueryFirstAsync<T>(sql, param);
        }

        public async Task<T> QuerySingleAsync(string sql, object? param = null, CancellationToken cancellationToken = default)
        {
            return await connection.QuerySingleAsync<T>(sql, param);
        }
    }
}
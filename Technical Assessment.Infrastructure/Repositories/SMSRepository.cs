using Microsoft.Extensions.Configuration;
using Technical_Assessment.Core.Entities;
using Technical_Assessment.Core.Repositories;
using Technical_Assessment.Infrastructure.Data;

namespace Technical_Assessment.Infrastructure.Repositories
{
    public class SMSRepository : Repository<SMS>, ISMSRepository
    {
        public SMSRepository(IConfiguration configuration, IApplicationDbContext context) : base(configuration, context) { }

        public async Task<SMS> AddAsync(SMS entity)
        {
            var query = $"INSERT INTO SMSs ({nameof(SMS.PhoneNumber)}, {nameof(SMS.Message)}) VALUES (@PhoneNumber, @Message)";
            var param = new { entity.PhoneNumber, entity.Message };
            await ExecuteAsync(query, param);
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var query = $"DELETE FROM SMSs WHERE {nameof(SMS.ID)} = @ID";
            var param = new { ID = id };
            await ExecuteAsync(query, param);
        }

        public async Task<IReadOnlyList<SMS>> GetAllAsync()
        {
            var query = $"SELECT * FROM SMSs";
            return await QueryAsync(query);
        }

        public async Task<SMS> GetByIdAsync(int id)
        {
            var query = $"SELECT * FROM SMSs WHERE {nameof(SMS.ID)} = {id}";
            return await QuerySingleAsync(query);
        }

        public async Task UpdateAsync(SMS entity)
        {
            var query = $"UPDATE SMSs SET {nameof(SMS.PhoneNumber)} = @PhoneNumber, {nameof(SMS.Message)} = @Message WHERE {nameof(SMS.ID)} = @ID";
            var param = new { entity.ID, entity.PhoneNumber, entity.Message };
            await ExecuteAsync(query, param);
        }
    }
}

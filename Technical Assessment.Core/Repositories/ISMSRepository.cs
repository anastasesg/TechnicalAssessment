using Technical_Assessment.Core.Entities;

namespace Technical_Assessment.Core.Repositories
{
    public interface ISMSRepository : IRepository<SMS>
    {
        public Task<SMS> AddAsync(SMS entity);
        public Task DeleteAsync(int id);
        public Task<IReadOnlyList<SMS>> GetAllAsync();
        public Task<SMS> GetByIdAsync(int id);
        public Task UpdateAsync(SMS entity);
    }
}

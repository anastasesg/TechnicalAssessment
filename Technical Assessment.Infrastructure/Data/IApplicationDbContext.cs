using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Data;
using Technical_Assessment.Core.Entities;

namespace Technical_Assessment.Infrastructure.Data
{
    public interface IApplicationDbContext
    {
        public IDbConnection Connection { get; }
        DatabaseFacade Database { get; }
        public DbSet<SMS> SMSs { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

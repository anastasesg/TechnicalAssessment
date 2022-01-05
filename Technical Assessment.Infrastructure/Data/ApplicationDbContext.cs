using Microsoft.EntityFrameworkCore;
using System.Data;
using Technical_Assessment.Core.Entities;

namespace Technical_Assessment.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<SMS> SMSs { get; set; }
        public IDbConnection Connection => Database.GetDbConnection();
    }
}

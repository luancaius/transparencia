using Deputies.Application.Ports.Out;
using Deputies.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Deputies.Adapter.Out.EFCoreSqlServer
{
    public class DeputyRepository : IDeputyRepository
    {
        private readonly ILogger<DeputyRepository> _logger;
        private readonly DeputiesDbContext _dbContext;

        public DeputyRepository(
            ILogger<DeputyRepository> logger,
            DeputiesDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task SaveDeputyAsync(Deputy deputy)
        {
            // EF Core example for SQL Server
            _dbContext.Deputies.Add(deputy);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Deputy {Id} saved.", deputy.MultiSourceId?.Ids);
        }
    }
}
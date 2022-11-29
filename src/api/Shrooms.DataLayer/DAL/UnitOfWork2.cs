using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shrooms.Contracts.DAL;

namespace Shrooms.DataLayer.DAL
{
    public class UnitOfWork2 : IUnitOfWork2
    {
        private readonly IDbContext _context;

        public UnitOfWork2(IDbContext context)
        {
            _context = context;
        }

        public string ConnectionName => _context.ConnectionName;

        public DbSet<TEntity> GetDbSet<TEntity>()
            where TEntity : class
        {
            return _context.Set<TEntity>();
        }

        public async Task<int> SaveChangesAsync(string userId)
        {
            return await _context.SaveChangesAsync(userId);
        }

        public async Task<int> SaveChangesAsync(bool useMetaTracking)
        {
            return await _context.SaveChangesAsync(useMetaTracking);
        }
    }
}

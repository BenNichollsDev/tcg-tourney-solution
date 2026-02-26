using System.Threading.Tasks;
using TCG.Application.Interfaces;

namespace TCG.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;

        public UnitOfWork(AppDbContext db)
        {
            _db = db;
        }

        public async Task<int> SaveChangesAsync()
            => await _db.SaveChangesAsync();
    }
}
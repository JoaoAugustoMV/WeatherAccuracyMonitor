using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WeatherAccuracyMonitorBackend.Domain.Repositories;

namespace WeatherAccuracyMonitorLib.Infra.AppDbContext.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected AppDbContext _appDbContext;
        protected DbSet<T> _dbSet;

        protected RepositoryBase(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _dbSet = _appDbContext.Set<T>();
        }
        
        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public async Task<T?> GetByIdAsync(string id)
        {
            return await _dbSet.FindAsync(id);            
        }
       
        public bool Delete(T entity)
        {
            _dbSet.Remove(entity);

            return _appDbContext.SaveChanges() > 0;
            
        }

        public async Task<T> InsertAsync(T entity)
        {
            EntityEntry<T> entityAdded = await _dbSet.AddAsync(entity);

            await _appDbContext.SaveChangesAsync();

            return entityAdded.Entity;            
        }

        public T Update(T entity)
        {
            EntityEntry<T> entityAdded = _dbSet.Update(entity);

            _appDbContext.SaveChanges();

            return entityAdded.Entity;
        }

        public async Task InsertAsync(IEnumerable<T> values)
        {
            await _appDbContext.AddRangeAsync(values);

            _appDbContext.SaveChanges();
        }
    }
}

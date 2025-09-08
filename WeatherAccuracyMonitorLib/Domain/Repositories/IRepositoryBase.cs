namespace WeatherAccuracyMonitorBackend.Domain.Repositories
{
    public interface IRepositoryBase<T>
    {
        Task<List<T>> GetAllAsync();
        IQueryable<T> GetAll();
        Task<T?> GetByIdAsync(string id);
        Task<T> InsertAsync(T entity);
        T Update(T entity);
        bool Delete(T entity);        
        Task InsertAsync(IEnumerable<T> values);

    }
}

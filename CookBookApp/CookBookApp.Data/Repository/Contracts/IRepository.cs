namespace CookBookApp.Data.Repository.Contracts;

public interface IRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> GetAll();

    IQueryable<TEntity> GetAllAsNoTracking();

    Task AddAsync(TEntity entity);

    Task AddRangeAsync(IEnumerable<TEntity> entities);

    void UpdateAsync(TEntity entity);

    void UpdateRangeAsync(IEnumerable<TEntity> entities);

    void RemoveAsync(TEntity entity);

    void RemoveRangeAsync(IEnumerable<TEntity> entities);

    Task SaveChangesAsync();
}
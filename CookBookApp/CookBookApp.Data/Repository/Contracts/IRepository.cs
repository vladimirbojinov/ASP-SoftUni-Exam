namespace CookBookApp.Data.Repository.Contracts;

public interface IRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> GetAll();

    IQueryable<TEntity> GetAllAsNoTracking();

    Task AddAsync(TEntity entity);

    Task AddRangeAsync(IEnumerable<TEntity> entities);

    void Update(TEntity entity);

    void UpdateRange(IEnumerable<TEntity> entities);

    void Remove(TEntity entity);

    void RemoveRange(IEnumerable<TEntity> entities);

    Task SaveChangesAsync();
}
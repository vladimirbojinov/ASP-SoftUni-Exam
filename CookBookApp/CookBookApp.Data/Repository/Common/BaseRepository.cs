using CookBookApp.Data.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CookBookApp.Data.Repository.Common;

public abstract class BaseRepository<TEntity>(
        CookBookDbContext dbContext
    ) : IRepository<TEntity> where TEntity : class
{
    protected CookBookDbContext DbContext { get; } = dbContext;

    protected DbSet<TEntity> DbSet { get; } = dbContext.Set<TEntity>();

    public async Task AddAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await DbSet.AddRangeAsync(entities);
    }

    public IQueryable<TEntity> GetAll()
    {
        return DbSet.AsQueryable();
    }

    public IQueryable<TEntity> GetAllAsNoTracking()
    {
        return DbSet.AsNoTracking()
                    .AsQueryable();
    }

    public void Remove(TEntity entity)
    {
        DbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        DbSet.RemoveRange(entities);
    }

    public async Task SaveChangesAsync()
    {
        await DbContext.SaveChangesAsync();
    }

    public void Update(TEntity entity)
    {
        DbContext.Update(entity);
    }

    public void UpdateRange(IEnumerable<TEntity> entities)
    {
        DbContext.UpdateRange(entities);
    }
}

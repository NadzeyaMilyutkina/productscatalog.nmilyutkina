using Microsoft.EntityFrameworkCore;

namespace ProductsCatalog.Client.Data.Contracts;

public interface IBaseDbContext
{
    public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class;

    public void SaveEntitiesChanges();
}
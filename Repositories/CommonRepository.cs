using ProductsCatalog.Client.Data;
using ProductsCatalog.Client.Domain.Contracts;

namespace ProductsCatalog.Client.Repositories;

public class CommonRepository<TEntity> : Contracts.ICommonRepository<TEntity> where TEntity : class, IBaseEntity
{
    private readonly ApplicationDbContext _dbContext;

    public CommonRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public TResult Execute<TResult>(Queries.Contracts.IQuery<TResult, TEntity> query)
    {
        return query.Generate(_dbContext);
    }
}

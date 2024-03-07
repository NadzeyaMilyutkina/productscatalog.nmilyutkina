using ProductsCatalog.Client.Data.Contracts;
using ProductsCatalog.Client.Domain.Contracts;

namespace ProductsCatalog.Client.Queries.Contracts;

// CRS was used here
public interface IQuery<TResult, TEntity> where TEntity : IBaseEntity
{
    TResult Generate(IBaseDbContext context);
}

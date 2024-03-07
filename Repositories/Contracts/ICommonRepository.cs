
using ProductsCatalog.Client.Domain.Contracts;

namespace ProductsCatalog.Client.Repositories.Contracts;

public interface ICommonRepository<TEntity> where TEntity : class, IBaseEntity
{
     TResult Execute<TResult>(Queries.Contracts.IQuery<TResult, TEntity> query);
}
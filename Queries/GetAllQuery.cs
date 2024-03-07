using ProductsCatalog.Client.Data.Contracts;
using ProductsCatalog.Client.Domain.Contracts;

namespace ProductsCatalog.Client.Queries;

public class GetAllQuery<TBaseEntity> : Client.Queries.Contracts.IQuery<IQueryable<TBaseEntity>, TBaseEntity> where TBaseEntity : class, IBaseEntity
{
    public IQueryable<TBaseEntity> Generate(IBaseDbContext dbContext)
    {
        Console.WriteLine($"Call of Generate from GetAllQuery with type {typeof(TBaseEntity)}");

        return dbContext.GetDbSet<TBaseEntity>().Where(e => true).AsQueryable();
    }
}

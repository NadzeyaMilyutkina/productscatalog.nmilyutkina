using ProductsCatalog.Client.Data.Contracts;
using ProductsCatalog.Client.Domain.Contracts;

namespace ProductsCatalog.Domain.Queries;

public class GetByIdQuery<TBaseEntity> : Client.Queries.Contracts.IQuery<TBaseEntity, TBaseEntity> where TBaseEntity : class, IBaseEntity
{
    private Guid id;

    public GetByIdQuery(Guid id)
    {
        this.id = id;
    }

    public TBaseEntity Generate(IBaseDbContext dbContext)
    {
        Console.WriteLine($"Call of Generate from GetByIdQuery with type {typeof(TBaseEntity)} and id = {id}");

        return dbContext.GetDbSet<TBaseEntity>().SingleOrDefault(e => e.Id == id)!;
    }
}
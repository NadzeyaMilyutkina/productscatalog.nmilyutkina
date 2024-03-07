using ProductsCatalog.Client.Data.Contracts;
using ProductsCatalog.Client.Domain.Contracts;

namespace ProductsCatalog.Domain.Queries;

public class InsertQuery<TBaseEntity> : Client.Queries.Contracts.IQuery<TBaseEntity, TBaseEntity> where TBaseEntity : class, IBaseEntity
{
    private TBaseEntity entity;

    public InsertQuery(TBaseEntity entity)
    {
        this.entity = entity;
    }

    public TBaseEntity Generate(IBaseDbContext dbContext)
    {
        Console.WriteLine($"Call of Generate from InsertQuery with type {typeof(TBaseEntity)}");

        var insertedEntity = dbContext.GetDbSet<TBaseEntity>().Add(entity).Entity;

        dbContext.SaveEntitiesChanges();

        return insertedEntity;
    }
}

using ProductsCatalog.Client.Data.Contracts;
using ProductsCatalog.Client.Domain.Contracts;

namespace ProductsCatalog.Client.Queries;

public class DeleteQuery<TBaseEntity>: Client.Queries.Contracts.IQuery<TBaseEntity, TBaseEntity> where TBaseEntity : class, IBaseEntity
{
    private TBaseEntity entity;

    private Guid id;

    public DeleteQuery(Guid id)
    // public DeleteQuery(TBaseEntity entity, Guid id)
    {
        // this.entity = entity;
        this.id = id;
    }

    public TBaseEntity Generate(IBaseDbContext dbContext)
    {
        Console.WriteLine($"Call of Generate from DeleteQuery with type {typeof(TBaseEntity)}");
        entity = dbContext.GetDbSet<TBaseEntity>().Single(x => x.Id.Equals(id));

        var deletedEntity = dbContext.GetDbSet<TBaseEntity>().Remove(entity).Entity;

        dbContext.SaveEntitiesChanges();

        return deletedEntity;
    }
}
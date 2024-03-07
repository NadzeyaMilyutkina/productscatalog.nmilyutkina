using ProductsCatalog.Client.Data.Contracts;
using ProductsCatalog.Client.Domain.Contracts;

namespace ProductsCatalog.Domain.Queries;

public class UpdateQuery<TBaseEntity> : Client.Queries.Contracts.IQuery<TBaseEntity, TBaseEntity> where TBaseEntity : class, IBaseEntity
{
    private TBaseEntity entity;

    public UpdateQuery(TBaseEntity entity)
    {
        this.entity = entity;
    }

    public TBaseEntity Generate(IBaseDbContext dbContext)
    {
        Console.WriteLine($"Call of Generate from UpdateQuery with type {typeof(TBaseEntity)}");

        var updatedEntity = dbContext.GetDbSet<TBaseEntity>().Update(entity).Entity;

        dbContext.SaveEntitiesChanges();

        return updatedEntity;
    }
}

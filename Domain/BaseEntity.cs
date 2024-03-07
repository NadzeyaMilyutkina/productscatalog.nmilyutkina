using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IBaseEntity = ProductsCatalog.Client.Domain.Contracts.IBaseEntity;

namespace ProductsCatalog.Client.Domain;

public abstract class BaseEntity : IBaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
}
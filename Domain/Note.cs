using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IBaseEntity = ProductsCatalog.Client.Domain.Contracts.IBaseEntity;

namespace ProductsCatalog.Client.Domain;

public class Note : IBaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public bool IsGeneral { get; set; }

    [DisplayName("Примечаниe")]
    public string Description { get; set; }
}
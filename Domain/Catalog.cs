using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IBaseEntity = ProductsCatalog.Client.Domain.Contracts.IBaseEntity;

namespace ProductsCatalog.Client.Domain;

public class Catalog : IBaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [DisplayName("Наименование каталога")]
    public String Name { get; set; }

    public IEnumerable<ProductCategory> ProductCategories { get; set; }
}

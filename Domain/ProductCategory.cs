using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IBaseEntity = ProductsCatalog.Client.Domain.Contracts.IBaseEntity;

namespace ProductsCatalog.Client.Domain;

public class ProductCategory : IBaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [DisplayName("Категория")]
    public string Name { get; set; }

    public Guid? CatalogId { get; set; }

    public Catalog Catalog { get; set; }

    public IEnumerable<Product>? Products { get; set; }

}

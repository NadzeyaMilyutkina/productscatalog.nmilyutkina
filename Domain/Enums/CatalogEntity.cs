using System.Text.Json.Serialization;

namespace ProductsCatalog.Client.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CatalogEntity
{
    ProductCategory = 0,
    Note = 1,
    Product = 2,
    Catalog = 3
}
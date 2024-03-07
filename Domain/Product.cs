using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Authorization;
using IBaseEntity = ProductsCatalog.Client.Domain.Contracts.IBaseEntity;

namespace ProductsCatalog.Client.Domain;

public class Product : IBaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [DisplayName("Наименование продукта")]
    public String Name { get; set; }

    [DisplayName("Категория")]
    public Guid? CategoryId { get; set; }

    public ProductCategory Category { get; set; }

    [DisplayName("Описание")]
    public String? Description { get; set; }

    [DisplayName("Стоимость в рублях")]
    public decimal? Price { get; set; }

    // public Note? GeneralNoteId { get; set; }
    //
    // public Note? SpecialNoteId { get; set; }

    [DisplayName("Примечание общее")]
    public Note? GeneralNote { get; set; }

    [DisplayName("Примечание специальное")]
    public Note? SpecialNote { get; set; }
}

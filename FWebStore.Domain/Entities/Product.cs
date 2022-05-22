using System.ComponentModel.DataAnnotations.Schema;
using FWebStore.Domain.Entities.Base;
using FWebStore.Domain.Entities.Base.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FWebStore.Domain.Entities;

//Для работы с индексами БД нужен пакет Microsoft.EntityFrameworkCore.Abstractions
[Index(nameof(Name))]
public class Product : NamedEntity, IOrderedEntity
{
    public int Order { get; set; }

    public int SectionId { get; set; }
    [ForeignKey(nameof(SectionId))]
    public Section Section { get; set; } //Навигационное свойство
    
    public int? BrandId { get; set; }

    [ForeignKey(nameof(BrandId))]
    public Brand Brand { get; set; } //Навигационное свойство

    public string ImageUrl { get; set; }

    [Column(TypeName = "decimal(18,2)")] //Указываем тип данных для столбца Price
    public decimal Price { get; set; }
}

